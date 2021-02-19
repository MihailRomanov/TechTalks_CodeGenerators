using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;
using Microsoft.VisualStudio.Web.CodeGeneration.DotNet;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CmdScaffolders.TemplateBasedGenerator
{
    [Alias("adv")]
    public class AdvancedGenerator: ICodeGenerator
    {
        private readonly IModelTypesLocator modelTypesLocator;
        private readonly IApplicationInfo applicationInfo;
        private readonly IProjectContext projectContext;
        private readonly ICodeGeneratorActionsService codeGeneratorActionsService;

        private const string TemplateName = "Template.cshtml";

        public AdvancedGenerator(IModelTypesLocator modelTypesLocator,
            IApplicationInfo applicationInfo,
            IProjectContext projectContext,
            ICodeGeneratorActionsService codeGeneratorActionsService)
        {
            this.modelTypesLocator = modelTypesLocator;
            this.applicationInfo = applicationInfo;
            this.projectContext = projectContext;
            this.codeGeneratorActionsService = codeGeneratorActionsService;
        }


        public void GenerateCode(AdvancedGeneratorModel model)
        {
            var types = modelTypesLocator.GetAllTypes();
            if (!string.IsNullOrEmpty(model.TypeFilter))
            {
                var regex = new Regex(model.TypeFilter);
                types = types.Where(t => regex.IsMatch(t.Name));
            }

            var templateModel = new TemplateModel
            {
                Namespace = model.Namespace,
                Types = types.Select(t =>
                        new TemplateModelType
                        {
                            Name = t.Name,
                            Properties = (t.TypeSymbol as INamedTypeSymbol)
                                .GetMembers()
                                .Where(m => m.Kind == SymbolKind.Property)
                                .OfType<IPropertySymbol>()
                                .Select(m => new TemplateModelProperty { Name = m.Name, Type = m.Type.Name }).ToArray()
                        }).ToArray()
            };

            var templateFolders = TemplateFoldersUtilities
                .GetTemplateFolders(this.GetType().Assembly.GetName().Name,
                    applicationInfo.ApplicationBasePath,
                    new string[] { "Advanced" },
                    projectContext);

            var outputFilePath = Path.Combine(applicationInfo.ApplicationBasePath, model.OutputFile);
            codeGeneratorActionsService.AddFileFromTemplateAsync(outputFilePath, TemplateName, templateFolders, templateModel);
        }
    }
}
