using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

if (args.Length == 0)
{
    Console.WriteLine("Usage: dotnet run --project tools/CodeFixer <path-to-src>");
    return;
}

string folder = args[0];
foreach (var file in Directory.GetFiles(folder, "*.cs", SearchOption.AllDirectories))
{
    var code = await File.ReadAllTextAsync(file);
    var tree = CSharpSyntaxTree.ParseText(code);
    var root = tree.GetRoot();

    var rewriter = new FixerRewriter();
    var newRoot = rewriter.Visit(root);

    var newCode = newRoot.NormalizeWhitespace().ToFullString();

    if(newCode != code)
    {
        await File.WriteAllTextAsync(file, newCode, Encoding.UTF8);
        Console.WriteLine($"Fixed:{file}");
    }
}

public class FixerRewriter : CSharpSyntaxRewriter
{
    public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        var updated = node;

        if(node.Modifiers.Any(SyntaxKind.AsyncKeyword) && ! node.Identifier.Text.EndsWith("Async"))
        {
            updated = updated.WithIdentifier(SyntaxFactory.Identifier(node.Identifier.Text + "Async"));
        }

        return base.VisitMethodDeclaration(updated);
    }

    public override SyntaxToken VisitToken(SyntaxToken token)
    {
        if(token.Text.Contains("Vm") || token.Text.Contains("Dto"))
        {
            var text = token.Text
                .Replace("Vm", "VM")
                .Replace("Vms", "VMS")
                .Replace("Dto", "DTO")
                .Replace("Dtos", "DTOs");

                return SyntaxFactory.Identifier(text);
        }
        return base.VisitListSeparator(token);
    }
}
