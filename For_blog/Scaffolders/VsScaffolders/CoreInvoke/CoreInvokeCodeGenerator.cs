using Microsoft.AspNet.Scaffolding;
using System.Windows;

namespace VsScaffolders.CoreInvoke
{
    public class CoreInvokeCodeGenerator : CodeGenerator
    {
        public CoreInvokeCodeGenerator(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {
        }

        public override void GenerateCode()
        {
            MessageBox.Show("Generate!!!");
        }

        public override bool ShowUIAndValidate()
        {
            return true;
        }
    }
}
