using Microsoft.AspNet.Scaffolding;
using Microsoft.WebTools.Scaffolding.Core;
using Microsoft.WebTools.Scaffolding.Core.CommandLine;
using Microsoft.WebTools.Scaffolding.Core.FactoryConfig;
using Microsoft.WebTools.Scaffolding.Core.Scaffolders;
using Microsoft.WebTools.Scaffolding.Shared.ProjectModel;
using System.ComponentModel.Composition;

namespace VsScaffolders.CoreInvoke
{
    public abstract class BaseCoreInvokeCodeGeneratorFactory : LegacyCodeGeneratorFactory, IConfigurableScaffolderFactory
    {
        public BaseCoreInvokeCodeGeneratorFactory(CodeGeneratorInformation codeGeneratorInformation)
            : base(codeGeneratorInformation)
        {
        }

        [Import]
        protected ICommandLineInvoker CommandLineInvoker { get; set; }

        [Import]
        protected IFileSystemChangeExecutor FileSystemChangeExecutor { get; set; }

        protected abstract ICodeGenerator CreateInstanceInternal(
            CodeGenerationContext context, CodeGeneratorInformation information);

        protected abstract IMvcCoreCodeGenerationActionService
            GetMvcCoreCodeGenerationActionService(CodeGenerationContext context);

        protected abstract IProjectContextBuilder GetProjectContextBuilder(CodeGenerationContext context);

        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            if (!context.Items.ContainsProperty(typeof(ICommandLineInvoker)))
                context.Items.AddProperty(typeof(ICommandLineInvoker), CommandLineInvoker);
            if (!context.Items.ContainsProperty(typeof(IFileSystemChangeExecutor)))
                context.Items.AddProperty(typeof(IFileSystemChangeExecutor), FileSystemChangeExecutor);
            if (!context.Items.ContainsProperty(typeof(IMvcCoreCodeGenerationActionService)))
                context.Items.AddProperty(typeof(IMvcCoreCodeGenerationActionService), GetMvcCoreCodeGenerationActionService(context));
            if (!context.Items.ContainsProperty(typeof(IProjectContextBuilder)))
                context.Items.AddProperty(typeof(IProjectContextBuilder), GetProjectContextBuilder(context));

            return CreateInstanceInternal(context, Information);
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
