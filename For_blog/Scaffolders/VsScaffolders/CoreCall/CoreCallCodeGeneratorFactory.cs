using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.Imaging.Interop;
using System;
using System.ComponentModel.Composition;

namespace VsScaffolders.CoreCall
{

    public class CodeGeneratorInformationProvider : BaseCodeGeneratorInformationProvider
    {
        public override CodeGeneratorInformation GetCodeGeneratorInformation()
        {
            return new CodeGeneratorInformation(
                "Core call Code Generator",
                "Call the console",
                "Some authors",
                new Version(1, 0, 0),
                nameof(CoreCallCodeGenerator),
                new ImageMoniker { Guid = new Guid("0808e3b1-36e0-4801-b042-faa5e505a64e"), Id = 0 },
                null,
                new[] { "SKB Kontur/Scaffolders/Simple" },
                true);
        }
    }

    [Export("NetCoreScaffolderFactory", typeof(LegacyCodeGeneratorFactory))]
    public class CoreCallCodeGeneratorFactory
        : BaseCoreCallCodeGeneratorFactory<CodeGeneratorInformationProvider, CoreCallCodeGenerator>
    {
    }

}
