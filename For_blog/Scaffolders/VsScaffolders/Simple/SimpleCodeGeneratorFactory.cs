using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.Imaging.Interop;
using System;
using System.ComponentModel.Composition;

namespace VsScaffolders.Simple
{
    [Export(typeof(LegacyCodeGeneratorFactory))]
    public class SimpleCodeGeneratorFactory : LegacyCodeGeneratorFactory
    {
        public SimpleCodeGeneratorFactory() :
            base(new CodeGeneratorInformation(
                "Simple Code Generator",
                "Generator",
                "Some authors",
                new Version(1, 0, 0),
                nameof(SimpleCodeGenerator),
                new ImageMoniker { Guid = new Guid("0808e3b1-36e0-4801-b042-faa5e505a64e"), Id = 0 },
                null,
                new[] { "SKB Kontur/Scaffolders/Simple" },
                true))
        {
        }

        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            return new SimpleCodeGenerator(context, Information);
        }
    }
}
