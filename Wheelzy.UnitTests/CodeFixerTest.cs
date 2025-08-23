
using Microsoft.CodeAnalysis.CSharp;

namespace Wheelzy.UnitTests
{
    public class CodeFixerTest
    {
        [Fact]
        public void AsyncMethodWithoutSuffix_IsRenamed()
        {
            var code = @"class C { public async Task Run() { } }";

            var tree = CSharpSyntaxTree.ParseText(code);

            var root = tree.GetRoot();
            var rewriter = new FixerRewriter();
            var newRoot = rewriter.Visit(root);

            Assert.Contains("RunAsync", newRoot.ToFullString());
        }

        [Fact]
        public void VmIsReplacedWithVM()
        {
            var code = @"class CarVm { } ";

            var tree = CSharpSyntaxTree.ParseText(code);

            var root = tree.GetRoot();

            var rewriter = new FixerRewriter();

            var newRoot = rewriter.Visit(root);

            Assert.Contains("CarVM", newRoot.ToFullString());
        }

        [Fact]
        public void DtoReplaceWithDTO()
        {
            var code = @"class CarDto { }";

            var tree = CSharpSyntaxTree.ParseText(code);

            var root = tree.GetRoot();

            var rewriter = new FixerRewriter();

            var newRoot = rewriter.Visit(root);

            Assert.Contains("CarDTO", newRoot.ToFullString());
        }
    }
}
