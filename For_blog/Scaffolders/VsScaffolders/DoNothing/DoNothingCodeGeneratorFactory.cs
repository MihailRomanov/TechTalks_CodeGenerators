using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.WebTools.Scaffolding.Core.FactoryConfig;
using Microsoft.WebTools.Scaffolding.Core.Scaffolders;
using System;
using System.ComponentModel.Composition;

namespace VsScaffolders.DoNothing
{
    [Export(typeof(CodeGeneratorFactory))]
    [Export("NetCoreScaffolderFactory", typeof(CodeGeneratorFactory))]
    public class DoNothingCodeGeneratorFactory : CodeGeneratorFactory, IConfigurableScaffolderFactory
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
                new[] { "SKB Kontur/Scaffolders/Simple" }))
        {
        }

        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            return new DoNothingCodeGenerator(context, Information);
        }

        public bool IsEvaluationContextInitialized(IScaffolderFactoryConfigEvaluationContext factoryEvaluationContext)
        {
            return true;
        }

        public bool IsSupported(IScaffolderFactoryConfigEvaluationContext facotryEvaluationContext)
        {
            return true;
        }

        public IScaffolderFactoryConfigEvaluationContext SetupConfigEvaluationContext(CodeGenerationContext context)
        {
            return null;
        }
    }
}
