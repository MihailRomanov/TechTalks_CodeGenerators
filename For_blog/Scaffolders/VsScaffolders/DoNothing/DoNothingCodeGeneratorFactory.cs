using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.Imaging.Interop;
using System;
using System.ComponentModel.Composition;

namespace VsScaffolders.DoNothing
{
    [Export(typeof(LegacyCodeGeneratorFactory))]
    public class DoNothingCodeGeneratorFactory : LegacyCodeGeneratorFactory
    {
        public DoNothingCodeGeneratorFactory() :
            base(new CodeGeneratorInformation(
                "DoNothing Code Generator",
                "Generator that doesn't do anything",
                "Some authors",
                new Version(1, 0, 0),
                nameof(DoNothingCodeGenerator),
                new ImageMoniker { Guid = new Guid("0808e3b1-36e0-4801-b042-faa5e505a64e"), Id = 0 },
                null,
                new[] { "SKB Kontur/Scaffolders/Simple" },
                true))
        {
        }

        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            return new DoNothingCodeGenerator(context, Information);
        }
    }
}
