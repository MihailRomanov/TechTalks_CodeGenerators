using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.PlatformUI;
using System.Linq;
using VsScaffolders.Model;
using VsScaffolders.UI;

namespace VsScaffolders.Simple
{
    public class SimpleCodeGenerator : CodeGenerator
    {
        private SelectCodeModelViewModel ViewModel { get; } = new SelectCodeModelViewModel();

        public SimpleCodeGenerator(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {
        }

        public override void GenerateCode()
        {
            MessageDialog.Show("Typed type name", ViewModel.SelectedType.Name, MessageDialogCommandSet.Ok);
        }

        public override bool ShowUIAndValidate()
        {
            var codeTypeService = (ICodeTypeService)ServiceProvider.GetService(typeof(ICodeTypeService));
            var reflectedTypesService = (IReflectedTypesService)ServiceProvider.GetService(typeof(IReflectedTypesService));

            var types = codeTypeService.GetAllCodeTypes(Context.ActiveProject).ToArray();
            var models = types.Select(t => CodeTypeModel.FromCodeType(t, reflectedTypesService)).ToArray();

            ViewModel.TypeModels = models;

            var dialog = new SelectCodeModelDialog();
            dialog.DataContext = ViewModel;

            return dialog.ShowModal() ?? false;
        }
    }
}
