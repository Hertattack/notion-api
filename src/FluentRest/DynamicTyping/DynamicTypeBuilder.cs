using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FluentRest.DynamicTyping
{
    public class DynamicTypeBuilder
    {
        public DynamicTypeBuilder()
        {
        }

        public string RoslynBuildDefinition<TBodyType>()
        {
            var type = typeof(TBodyType);

            var ns = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName($"DynamicTypes.{type.Namespace}"));

            ns.AddUsings(
                SyntaxFactory.UsingDirective(
                    SyntaxFactory.ParseName(type.Namespace ?? throw new InvalidOperationException())),
                SyntaxFactory.UsingDirective(
                    SyntaxFactory.ParseName(typeof(IDynamicType).Namespace ?? throw new InvalidOperationException())));

            var classDeclaration = SyntaxFactory.ClassDeclaration(type.Name);
            classDeclaration = classDeclaration.AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

            classDeclaration = classDeclaration.AddBaseListTypes(
                SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(nameof(IDynamicType))),
                SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(type.Name)));

            var constructor = SyntaxFactory.ConstructorDeclaration(type.Name);

            var constructorParameter = SyntaxFactory.Parameter(SyntaxFactory.Identifier("spec"));
            constructorParameter =
                constructorParameter.WithType(SyntaxFactory.IdentifierName("ISpec"));

            constructor = constructor.WithParameterList(
                SyntaxFactory.ParameterList(
                    SyntaxFactory.SeparatedList<ParameterSyntax>(new[] {constructorParameter})));

            constructor = constructor.WithModifiers(SyntaxTokenList.Create(SyntaxFactory.ParseToken("public")));

            var block = SyntaxFactory.Block();
            block = block.AddStatements(
                SyntaxFactory.ExpressionStatement(SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, SyntaxFactory.IdentifierName("a"), SyntaxFactory.IdentifierName("b")))
            );
            
            constructor = constructor.WithBody(block);
            
            classDeclaration = classDeclaration.AddMembers(constructor);
            
            ns = ns.AddMembers(classDeclaration);

            return ns
                .NormalizeWhitespace()
                .ToFullString();
        }
    }
}