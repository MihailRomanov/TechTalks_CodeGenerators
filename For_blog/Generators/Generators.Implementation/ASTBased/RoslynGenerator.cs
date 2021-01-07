using Generators.Common;
using Generators.Common.EntitiesModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System.Linq;

namespace Generators.Implementation.ASTBased
{
    public class RoslynGenerator : IGenerator<Model>
    {
        private readonly SyntaxGenerator generator;

        public RoslynGenerator()
            : this(LanguageNames.CSharp)
        { }

        public RoslynGenerator(string languageName)
        {
            var workspace = new AdhocWorkspace();
            generator = SyntaxGenerator.GetGenerator(workspace, languageName);
        }

        public string Generate(Model model)
        {
            var compilationUnit = GetCodeCompileUnit(model.Entities, model.NamespaceName);
            return compilationUnit.NormalizeWhitespace().ToFullString();
        }

        private SyntaxNode GetCodeCompileUnit(Entity[] entities, string namespaceName)
        {
            return generator.CompilationUnit(
                generator.NamespaceDeclaration(
                    namespaceName,
                    entities.Select(entity =>
                        generator.ClassDeclaration(
                            entity.Name,
                            members: entity.Properties?.Select(prop =>
                                generator.PropertyDeclaration(
                                    prop.Name,
                                    prop.DataType.ToTypeNode(generator),
                                    Accessibility.Public))
                    ))));
        }
    }
}
