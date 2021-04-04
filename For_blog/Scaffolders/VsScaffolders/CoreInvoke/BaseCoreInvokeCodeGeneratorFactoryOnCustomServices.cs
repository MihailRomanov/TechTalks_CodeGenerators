using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.WebTools.Scaffolding.Core;
using Microsoft.WebTools.Scaffolding.Shared.ProjectModel;
using NuGet.VisualStudio;
using System.ComponentModel.Composition;

namespace VsScaffolders.CoreInvoke
{
    public abstract class BaseCoreInvokeCodeGeneratorFactoryOnCustomServices : BaseCoreInvokeCodeGeneratorFactory
    {
        protected BaseCoreInvokeCodeGeneratorFactoryOnCustomServices(CodeGeneratorInformation codeGeneratorInformation)
            : base(codeGeneratorInformation)
        {
        }

        [Import]
        private IMsBuildProjectPropertyService MsBuildProjectPropertyService { get; set; }

        [Import]
        private VisualStudioWorkspace Workspace { get; set; }

        [Import]
        private IVsPackageInstallerServices VsPackageInstallerServices { get; set; }


        protected override IMvcCoreCodeGenerationActionService GetMvcCoreCodeGenerationActionService(
            CodeGenerationContext context)
        {
            var fileSystemService = (IFileSystemService)context.ServiceProvider.GetService(typeof(IFileSystemService));
            return new MvcCoreCodeGenerationActionService(fileSystemService);
        }

        protected override IProjectContextBuilder GetProjectContextBuilder(CodeGenerationContext context)
        {
            return new ProjectContextBuilder(
                MsBuildProjectPropertyService, context.ActiveProject, context.ServiceProvider, Workspace,
                VsPackageInstallerServices);
        }
    }
}
