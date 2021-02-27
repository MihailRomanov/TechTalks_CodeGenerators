using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.PlatformUI;
using System.Windows;

namespace VsScaffolders.Simple
{
    public class SimpleCodeGenerator : CodeGenerator
    {
        public SimpleCodeGenerator(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {
        }

        public override void GenerateCode()
        {
            MessageBox.Show("Generate!!!");
        }

        public override bool ShowUIAndValidate()
        {
            var dialog = new Dialog();
            var result = dialog.ShowDialog();

            return result ?? false;
        }
    }
}
