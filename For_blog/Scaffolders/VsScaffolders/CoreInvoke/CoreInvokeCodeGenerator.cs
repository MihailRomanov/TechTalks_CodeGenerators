using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.PlatformUI;

namespace VsScaffolders.CoreInvoke
{
    public class CoreInvokeCodeGenerator : BaseCoreInvokeCodeGenerator
    {
        public CoreInvokeCodeGenerator(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {
        }

        protected override string CodeGeneratorPackageName => "CmdScaffolders";

        protected override string CodeGeneratorPackageVersion => "1.0.0";

        public override void GenerateCode()
        {
            InvokeCoreGenerator("report MyReport -t");
        }

        public override bool ShowUIAndValidate()
        {
            return MessageDialog.Show("Coninue?", "Continue", MessageDialogCommandSet.YesNo) == MessageDialogCommand.Yes;
        }
    }
}
