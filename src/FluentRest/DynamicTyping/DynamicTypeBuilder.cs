using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentRest.DynamicTyping.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FluentRest.DynamicTyping
{
    public class DynamicTypeBuilder
    {
        public DynamicTypeBuilder()
        {
        }

        public Type RoslynBuildDefinition<TBodyType>()
        {
            var assemblyBuilder = new AssemblyBuilder();

            var classBuilder = assemblyBuilder.GetClassBuilder<TBodyType>("DynamicType");
            
            classBuilder
                .InheritsFrom<TBodyType>()
                .InheritsFrom<IDynamicType>()
                .WithParameter<ISpec>("__spec")
                .WithParameter<ITypeCache>("__typeCache");
            
            var type = typeof(TBodyType);

            // Implement property:
            foreach (var property in type.GetProperties(BindingFlags.Public))
            {
                classBuilder.Implement(property, @$"
                    __spec.SetContext(this, DynamicTyping.Operation.PropertyGet, ""{property.Name}"");
                    return __typeCache.GetInstance<{property.PropertyType.FullName}>(__spec, __typeCache);");
            }

            var className = type.Name[1..];
            var namespaceName = $"DynamicTypes.{type.Namespace}.{type.Name[1..]}";

            var compilationUnit = SyntaxFactory.CompilationUnit();
            compilationUnit = compilationUnit.AddUsings(
                SyntaxFactory.UsingDirective(
                    SyntaxFactory.ParseName(type.Namespace ?? throw new InvalidOperationException())),
                SyntaxFactory.UsingDirective(
                    SyntaxFactory.ParseName(typeof(IDynamicType).Namespace ?? throw new InvalidOperationException())));
            
            var ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(namespaceName));

            var classDeclaration = SyntaxFactory.ClassDeclaration(className);
            classDeclaration = classDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

            classDeclaration = classDeclaration.AddBaseListTypes(
                SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(nameof(IDynamicType))),
                SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(type.Name)));

            var constructor = SyntaxFactory.ConstructorDeclaration(className);

            var constructorParameter = SyntaxFactory.Parameter(SyntaxFactory.Identifier("spec"));
            constructorParameter =
                constructorParameter.WithType(SyntaxFactory.IdentifierName("ISpec"));

            constructor = constructor.WithParameterList(
                SyntaxFactory.ParameterList(
                    SyntaxFactory.SeparatedList(new[] {constructorParameter})));

            constructor = constructor.WithModifiers(SyntaxTokenList.Create(SyntaxFactory.ParseToken("public")));

            var block = SyntaxFactory.Block();
            block = block.AddStatements(
                SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName("a"),
                    SyntaxFactory.IdentifierName("b")))
            );

            constructor = constructor.WithBody(block);

            classDeclaration = classDeclaration.AddMembers(constructor);

            ns = ns.AddMembers(classDeclaration);
            compilationUnit = compilationUnit.AddMembers(ns);
            
            Console.WriteLine( ns.NormalizeWhitespace().ToFullString());
            
            var dynamicAssembly = GenerateCode(compilationUnit.SyntaxTree.GetRoot().SyntaxTree);
            var dynamicType = dynamicAssembly.GetType($"{namespaceName}.{className}");
            var obj = (TBodyType) dynamicType.GetConstructor(new[] {typeof(ISpec)}).Invoke(new[] {new Spec()});
            
            return dynamicType;
        }

        static Assembly GenerateCode(SyntaxTree tree)
        {
            var references = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic)
                .Select(a => MetadataReference.CreateFromFile(a.Location)).ToArray();

            var compilation = CSharpCompilation.Create("Hello.dll",
                new[] {tree},
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary,
                    optimizationLevel: OptimizationLevel.Release,
                    assemblyIdentityComparer: DesktopAssemblyIdentityComparer.Default));

            using var ms = new MemoryStream();
            var result = compilation.Emit(ms);

            if (!result.Success)
            {
                var failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                foreach (Diagnostic diagnostic in failures)
                {
                    Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                }

                throw new Exception("Compilation failed");
            }
            else
            {
                ms.Seek(0, SeekOrigin.Begin);
                return Assembly.Load(ms.ToArray());
            }
        }
    }

    public class Spec : ISpec
    {
    }

    public interface ISpec
    {
    }
}