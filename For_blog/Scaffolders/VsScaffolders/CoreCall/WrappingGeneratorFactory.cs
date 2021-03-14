using Microsoft.AspNet.Scaffolding;
using Microsoft.WebTools.Scaffolding.Core;
using Microsoft.WebTools.Scaffolding.Core.Scaffolders.Area;
using System;
using System.ComponentModel.Composition;

namespace VsScaffolders.CoreCall
{
    [Export(typeof(WrappingGeneratorFactory<,>))]
    public class WrappingGeneratorFactory<TProvider, TGenerator> : ScaffolderFactory
        where TProvider : BaseCodeGeneratorInformationProvider, new()
        where TGenerator : BaseCoreCallCodeGenerator

    {
        public WrappingGeneratorFactory() :
            base(new TProvider().GetCodeGeneratorInformation())
        {
        }

        public override string FactoryConfigKey => nameof(AreaScaffolderFactory);

        protected override ICodeGenerator CreateInstanceInternal(CodeGenerationContext context)
        {
            return (TGenerator)Activator.CreateInstance(typeof(TGenerator), new object[] { context, Information });
        }
    }

}
