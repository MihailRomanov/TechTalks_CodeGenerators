using Microsoft.AspNet.Scaffolding;
using System;
using System.Collections.Generic;
using System.IO;
using VsScaffolders.Model;
using VsScaffolders.UI;

namespace VsScaffolders.TemplateBased
{
    public class TemplateBasedGenerator : CodeGenerator
    {
        private const string ReportFolderName = "Reports";
        private const string TemplatesFolderName = "Report";

        private SelectCodeModelViewModel ViewModel { get; } = new SelectCodeModelViewModel();

        public TemplateBasedGenerator(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {
        }

        public override void GenerateCode()
        {
            var reportName = $"Report_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}";

            AddFolder(Context.ActiveProject, ReportFolderName);

            var templateParams = new Dictionary<string, object>
            {
                ["Model"] = new CodeTypeModel(ViewModel.SelectedType.Name, ViewModel.SelectedProperties)
            };
            AddFileFromTemplate(Context.ActiveProject, Path.Combine(ReportFolderName, reportName),
                "ReportTemplate", templateParams, false);
        }

        public override bool ShowUIAndValidate()
        {
            var codeTypeService = (ICodeTypeService)ServiceProvider.GetService(typeof(ICodeTypeService));
            var reflectedTypesService = (IReflectedTypesService)ServiceProvider.GetService(typeof(IReflectedTypesService));

            ViewModel.TypeModels = ModelUtils.GetCodeTypeModels(Context.ActiveProject, codeTypeService, reflectedTypesService);

            var dialog = new SelectCodeModelDialog();
            dialog.DataContext = ViewModel;

            return dialog.ShowModal() ?? false;
        }

        public override IEnumerable<string> TemplateFolders
        {
            get
            {
                var baseTemplatePath = Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location),
                    "Templates", TemplatesFolderName);
                var projectTemplatePath = Path.Combine(Context.ActiveProject.GetFullPath(),
                    "Templates", TemplatesFolderName);
                return new[] { projectTemplatePath, baseTemplatePath };
            }
        }
    }
}
