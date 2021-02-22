using Microsoft.AspNet.Scaffolding;
using System;
using System.ComponentModel.Composition;

namespace VsScaffolders.DoNothing
{
    [Export(typeof(CodeGeneratorFactory))]
    public class DoNothingCodeGeneratorFactory : CodeGeneratorFactory
    {
        public DoNothingCodeGeneratorFactory() :
            base(new CodeGeneratorInformation(
                "DoNothing Code Generator", "Generator that doesn't do anything",
                "Some authors", new Version(1, 0, 0), nameof(DoNothingCodeGenerator)))
        {
        }

        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            return new DoNothingCodeGenerator(context, Information);
        }
    }
}
