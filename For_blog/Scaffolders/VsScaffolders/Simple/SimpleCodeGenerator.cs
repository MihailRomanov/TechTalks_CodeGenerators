using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.PlatformUI;

namespace VsScaffolders.Simple
{
    public class SimpleCodeGenerator : CodeGenerator
    {
        private string typeName;

        public SimpleCodeGenerator(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {
        }

        public override void GenerateCode()
        {
            MessageDialog.Show("Typed type name", typeName, MessageDialogCommandSet.Ok);
        }

        public override bool ShowUIAndValidate()
        {
            return TextInputDialog.Show("Type name", "Full type name", "System.String", out typeName);
        }
    }
}
