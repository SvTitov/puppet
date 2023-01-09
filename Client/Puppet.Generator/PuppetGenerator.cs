using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Puppet.Generator.Utils;

namespace Puppet.Generator;

[Generator]
public class PuppetGenerator : ISourceGenerator
{
    
    private static readonly DiagnosticDescriptor InvalidXmlWarning = new("Puppet 1",
        title: "Puppet generation info",
        messageFormat: "{0}",
        category: "Generation",
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
        InjectAttributes(context);

        InjectClasses(context);

        InjectCodeForTargets(context);
    }

    private void InjectCodeForTargets(GeneratorExecutionContext context)
    {
        var pSyntax = (PuppetSyntaxReceiver)context.SyntaxReceiver!;
        
        context.ReportDiagnostic(Diagnostic.Create(InvalidXmlWarning, Location.None, $">- [Puppet] total classes: {pSyntax.Classes.Count}" ));
        
        foreach (var classDeclarationSyntax in pSyntax.Classes)
        {
            //TODO: Check that target is ContatePage 
            // Override onAppear and onDeapear

            
            //Example how to detect assemble
            // var testFramework = TestFramework.Unknown;
            // foreach (var assembly in testClass.ContainingModule.ReferencedAssemblies)
            // {
            //     if (assembly.Name == "Microsoft.VisualStudio.TestPlatform.TestFramework")
            //         testFramework = TestFramework.MSTest;
            //     else if (assembly.Name == "nunit.framework")
            //         testFramework = TestFramework.NUnit;
            //     else if (assembly.Name == "xunit.core")
            //         testFramework = TestFramework.XUnit;
            // }

            var baseClass = classDeclarationSyntax.BaseList?.Types.FirstOrDefault();
            
            if (baseClass?.Type.ToString() == "ContentPage")
            {
            }

            var namespaceName = ExtractNamespace(classDeclarationSyntax);
            
            SourceText sourceText = SourceText.From($@"

namespace {namespaceName};

public partial class {classDeclarationSyntax.Identifier}
{{
    {TargetClassSources.CONNECT_METHOD}

    {TargetClassSources.ON_CONNECTION_RESULT_METHOD}
}}", Encoding.UTF8);
            context.AddSource($"{classDeclarationSyntax.Identifier}.g.cs", sourceText);
        }
    }

    private void InjectClasses(GeneratorExecutionContext context)
    {
        context.AddSource
        (
            $"{ClassSources.CONNECTION_MANAGER_NAME}.g.cs", 
            SourceText.From(ClassSources.CONNECTION_MANAGER_IMPL, Encoding.UTF8)
        );
        
        context.AddSource
        (
            $"{ClassSources.SERVER_CONNECTOR_NAME}.g.cs",
            SourceText.From(ClassSources.SERVER_CONNECTOR_IMPL, Encoding.UTF8)
        );
        
        context.AddSource
        (
            $"{ClassSources.HTTP_BUILDER_NAME}.g.cs",
            SourceText.From(ClassSources.HTTP_BUILDER_IMPL, Encoding.UTF8)
        );
    }

    private void InjectAttributes(GeneratorExecutionContext context)
    {
        context.AddSource($"{Attributes.ATTRIBUTE_FULL_NAME}.g.cs",
            SourceText.From(Attributes.ATTRIBUTE_IMPL, Encoding.UTF8));
    }
   
    private string ExtractNamespace(BaseTypeDeclarationSyntax syntax)
    {
        var namespaceBuilder = new NamespaceBuilder();
        
        SyntaxNode? syntaxNode = syntax;
        while (syntaxNode != null)
        {
            if (syntaxNode is not BaseNamespaceDeclarationSyntax)
            {
                syntaxNode = syntaxNode.Parent;
            }
            else if (syntaxNode is BaseNamespaceDeclarationSyntax baseNamespaceDeclarationSyntax)
            {
                namespaceBuilder.Append(baseNamespaceDeclarationSyntax.Name.ToString());
                syntaxNode = syntaxNode.Parent;
            }
        }

        return namespaceBuilder.Build();
    }
}