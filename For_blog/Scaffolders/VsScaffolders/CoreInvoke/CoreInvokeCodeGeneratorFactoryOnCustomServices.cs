using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.Imaging.Interop;
using System;
using System.ComponentModel.Composition;

namespace VsScaffolders.CoreInvoke
{
    [Export("NetCoreScaffolderFactory", typeof(LegacyCodeGeneratorFactory))]
    public class CoreInvokeCodeGeneratorFactoryOnCustomServices : BaseCoreInvokeCodeGeneratorFactoryOnCustomServices
    {
        public CoreInvokeCodeGeneratorFactoryOnCustomServices() :
            base(new CodeGeneratorInformation(
                "Core Invoke Code Generator (on custom services)",
                "Generator that invoke ",
                "Some authors",
                new Version(1, 0, 0),
                nameof(CoreInvokeCodeGeneratorFactoryOnCustomServices),
                new ImageMoniker { Guid = new Guid("0808e3b1-36e0-4801-b042-faa5e505a64e"), Id = 0 },
                null,
                new[] { "SKB Kontur/Scaffolders/Simple" },
                true))
        {
        }

        protected override ICodeGenerator CreateInstanceInternal(CodeGenerationContext context,
            CodeGeneratorInformation information)
        {
            return new CoreInvokeCodeGenerator(context, information);
        }
    }
}
