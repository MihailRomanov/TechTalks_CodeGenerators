using Microsoft.AspNet.Scaffolding;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.WebTools.Scaffolding.Core;
using Microsoft.WebTools.Scaffolding.Core.FactoryConfig;
using Microsoft.WebTools.Scaffolding.Core.Scaffolders;
using NuGet.VisualStudio;
using System;
using System.ComponentModel.Composition;
using VsScaffolders.DoNothing;

namespace VsScaffolders.CoreInvoke
{
    [Export("NetCoreScaffolderFactory", typeof(LegacyCodeGeneratorFactory))]
    public class CoreInvokeCodeGeneratorFactory : LegacyCodeGeneratorFactory, IConfigurableScaffolderFactory
    {
        public CoreInvokeCodeGeneratorFactory() :
            base(new CodeGeneratorInformation(
                "CoreInvoke Code Generator",
                "Generator that doesn't do anything",
                "Some authors",
                new Version(1, 0, 0),
                nameof(CoreInvokeCodeGenerator),
                new ImageMoniker { Guid = new Guid("0808e3b1-36e0-4801-b042-faa5e505a64e"), Id = 0 },
                null,
                new[] { "SKB Kontur/Scaffolders/Simple" },
                true))
        {
        }

        [Import]
        private IMsBuildProjectPropertyService MsBuildProjectPropertyService { get; set; }

        [Import]
        private VisualStudioWorkspace Workspace { get; set; }

        [Import]
        private IVsPackageInstallerServices VsPackageInstallerServices { get; set; }

        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            var projectContextBuilder = new ProjectContextBuilder(
                MsBuildProjectPropertyService, context.ActiveProject, context.ServiceProvider, Workspace,
                VsPackageInstallerServices);

            var p = projectContextBuilder.BuildProjectContext();

            return new CoreInvokeCodeGenerator(context, Information);
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
