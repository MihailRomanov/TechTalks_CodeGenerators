using Microsoft.AspNet.Scaffolding;
using Microsoft.WebTools.Scaffolding.Core.FactoryConfig;
using Microsoft.WebTools.Scaffolding.Core.Scaffolders;
using System.ComponentModel.Composition;

namespace VsScaffolders.CoreCall
{
    public abstract class BaseCoreCallCodeGeneratorFactory<TProvider, TGenerator>
        : LegacyCodeGeneratorFactory, IConfigurableScaffolderFactory
        where TProvider : BaseCodeGeneratorInformationProvider, new()
        where TGenerator : BaseCoreCallCodeGenerator
    {
        public BaseCoreCallCodeGeneratorFactory() :
            base(new TProvider().GetCodeGeneratorInformation())
        {
        }

        [Import]
        internal WrappingGeneratorFactory<TProvider, TGenerator> WrappingFactory { get; set; }

        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            return WrappingFactory.CreateInstance(context);
        }

        public virtual bool IsEvaluationContextInitialized(IScaffolderFactoryConfigEvaluationContext factoryEvaluationContext)
        {
            return true;
        }

        public virtual bool IsSupported(IScaffolderFactoryConfigEvaluationContext facotryEvaluationContext)
        {
            return true;
        }

        public virtual IScaffolderFactoryConfigEvaluationContext SetupConfigEvaluationContext(CodeGenerationContext context)
        {
            return null;
        }
    }

}
