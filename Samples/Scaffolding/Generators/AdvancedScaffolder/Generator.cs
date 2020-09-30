using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;
using Microsoft.VisualStudio.Web.CodeGeneration.DotNet;

namespace AdvancedScaffolder
{
    [Alias("adv")]
    public class AdvancedCodeGenerator : ICodeGenerator
    {
        private IServiceProvider _serviceProvider;

        public AdvancedCodeGenerator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task GenerateCode(GeneratorModel model)
        {
            var typeLocator = _serviceProvider.GetRequiredService<IModelTypesLocator>();

            var genModel = new Model()
            {
                Namespace = model.Namespace,
                Types = typeLocator.GetAllTypes().Select(t =>
                    new MyType
                    {
                        Name = t.Name,
                        Properties = (t.TypeSymbol as INamedTypeSymbol)
                            .GetMembers()
                            .Where(m => m.Kind == SymbolKind.Property)
                            .OfType<IPropertySymbol>()
                            .Select(m => new MyProperty { Name = m.Name, Type = m.Type.Name }).ToArray()
                    }).ToArray()
            };

            var applicationInfo = _serviceProvider.GetRequiredService<IApplicationInfo>();
            var projectInfo = _serviceProvider.GetRequiredService<IProjectContext>();

            var templateFolders = TemplateFoldersUtilities
                .GetTemplateFolders("AdvancedScaffolder",
                    applicationInfo.ApplicationBasePath,
                    new string[] { "Adv" }, projectInfo);

            var actionService = _serviceProvider.GetRequiredService<ICodeGeneratorActionsService>();
            await actionService.AddFileFromTemplateAsync(@".\file.txt", "Template.cshtml", templateFolders, genModel);

        }

        public class GeneratorModel
        {
            [Argument(Description = "Namespace")]
            public string Namespace { get; set; }
        }
    }
}
