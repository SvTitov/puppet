using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Puppet.Generator;

public class PuppetSyntaxReceiver : ISyntaxReceiver
{
    public List<ClassDeclarationSyntax> Classes { get; } = new();
    
    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is ClassDeclarationSyntax syntaxClass)
        {
            var attributes = syntaxClass.AttributeLists
                .SelectMany(x => x.Attributes)
                .ToArray();
            
            if (attributes.Any())
            {
                foreach (var attribute in attributes)
                {
                    if (attribute.Name.ToFullString() == Attributes.ATTRIBUTE_NAME)
                    {
                        Classes.Add(syntaxClass);    
                    }
                }
            } 
        }
    }
}