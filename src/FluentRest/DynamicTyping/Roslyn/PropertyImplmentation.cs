using Microsoft.CodeAnalysis.CSharp;

namespace FluentRest.DynamicTyping.Roslyn
{
    class PropertyImplmentation
    {
        private readonly string _propertyName;

        public PropertyImplmentation(string propertyName, string cSharpCode)
        {
            _propertyName = propertyName;
            
            var implementation = CSharpSyntaxTree.ParseText(cSharpCode);
            var block = SyntaxFactory.Block();
        }
    }
}