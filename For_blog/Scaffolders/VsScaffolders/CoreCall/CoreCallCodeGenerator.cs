using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.PlatformUI;

namespace VsScaffolders.CoreCall
{

    public class CoreCallCodeGenerator : BaseCoreCallCodeGenerator
    {
        public CoreCallCodeGenerator(CodeGenerationContext context, CodeGeneratorInformation information)
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
