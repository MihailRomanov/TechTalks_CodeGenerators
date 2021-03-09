using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.WebTools.Scaffolding.Core.FactoryConfig;
using Microsoft.WebTools.Scaffolding.Core.Scaffolders;
using System;
using System.ComponentModel.Composition;

namespace VsScaffolders.Simple
{
    [Export(typeof(CodeGeneratorFactory))]
    [Export("NetCoreScaffolderFactory", typeof(CodeGeneratorFactory))]
    public class SimpleCodeGeneratorFactory : CodeGeneratorFactory, IConfigurableScaffolderFactory
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
                new[] { "SKB Kontur/Scaffolders/Simple" }))
        {
        }

        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            return new SimpleCodeGenerator(context, Information);
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
