using Microsoft.AspNet.Scaffolding;
using System;
using System.IO;
using System.Text;
using VsScaffolders.Model;
using VsScaffolders.UI;

namespace VsScaffolders.Simple
{
    public class SimpleCodeGenerator : CodeGenerator
    {
        private const string ReportFolderName = "Reports";

        private SelectCodeModelViewModel ViewModel { get; } = new SelectCodeModelViewModel();

        public SimpleCodeGenerator(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {
        }

        public override void GenerateCode()
        {
            var report = CreateReportContent(ViewModel);
            var reportName = $"Report_{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}.rpt";

            var fileService = (IFileSystemService)ServiceProvider.GetService(typeof(IFileSystemService));

            var tmpFileName = Path.GetTempFileName();
            fileService.WriteAllText(tmpFileName, report);

            AddFolder(Context.ActiveProject, ReportFolderName);
            AddFile(Context.ActiveProject, Path.Combine(ReportFolderName, reportName), tmpFileName, false);
        }

        private string CreateReportContent(SelectCodeModelViewModel viewModel)
        {
            var result = new StringBuilder();

            result.AppendLine($"Selected type: {viewModel.SelectedType.Name}");
            foreach (var property in viewModel.SelectedProperties)
            {
                result.AppendLine($"\t{property.Name} : {property.Type.FullName}");
            }

            return result.ToString();
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
    }
}
