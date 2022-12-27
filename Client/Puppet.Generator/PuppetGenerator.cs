using System.Diagnostics;
using System.Text;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Puppet.Generator;

[Generator]
public class PuppetGenerator : ISourceGenerator
{
    
    private static readonly DiagnosticDescriptor InvalidXmlWarning = new DiagnosticDescriptor(id: "MYXMLGEN001",
        title: "Super kek",
        messageFormat: "{0}",
        category: "Empty",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    
    public void Initialize(GeneratorInitializationContext context)
    {
#if DEBUG
        if (!Debugger.IsAttached)
        {
            Debugger.Launch();
        }
#endif
        
        context.RegisterForSyntaxNotifications(() => new PuppetSyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        // Add attribute
        context.AddSource($"{Attributes.ATTRIBUTE_FULL_NAME}.g.cs",  SourceText.From(Attributes.ATTRIBUTE_IMPL, Encoding.UTF8));

        var pSyntax = (PuppetSyntaxReceiver)context.SyntaxReceiver!;
        
        context.ReportDiagnostic(Diagnostic.Create(InvalidXmlWarning, Location.None, $">---- Total: {pSyntax.Classes.Count}" ));
        
        foreach (var classDeclarationSyntax in pSyntax.Classes)
        {
            SourceText sourceText = SourceText.From($@"
public partial class {classDeclarationSyntax.Identifier}
{{

    {HttpClientSources.CONNECT_METHOD}
}}", Encoding.UTF8);
            context.AddSource($"{classDeclarationSyntax.Identifier}.g.cs", sourceText);
        }
    }
    
    
    private IEnumerable<ClassDeclarationSyntax> GetClasses(GeneratorExecutionContext context) 
    {
        foreach (var tree in context.Compilation.SyntaxTrees) 
        {
            var semantic = context.Compilation.GetSemanticModel(tree);

            foreach (var foundClass in tree.GetRoot().DescendantNodesAndSelf().OfType<ClassDeclarationSyntax>()) 
            {
                var classSymbol = ModelExtensions.GetDeclaredSymbol(semantic, foundClass);

                if (classSymbol != null && classSymbol.GetAttributes().Any(attribute => attribute.AttributeClass?.Name == Attributes.ATTRIBUTE_FULL_NAME)) 
                {
                    yield return foundClass;
                }
            }
        }
    }
}