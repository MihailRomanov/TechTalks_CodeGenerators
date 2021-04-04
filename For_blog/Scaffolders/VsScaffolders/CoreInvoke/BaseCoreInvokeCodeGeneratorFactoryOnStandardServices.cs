using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.ProjectSystem.VS.Extensibility;
using Microsoft.WebTools.Scaffolding.Core;
using Microsoft.WebTools.Scaffolding.Core.CommandLine;
using Microsoft.WebTools.Scaffolding.Shared.ProjectModel;
using Microsoft.WebTools.Shared.CPS.VS.ProjectInfo;
using System;
using System.ComponentModel.Composition;

namespace VsScaffolders.CoreInvoke
{
    public abstract class BaseCoreInvokeCodeGeneratorFactoryOnStandardServices : BaseCoreInvokeCodeGeneratorFactory
    {
        public BaseCoreInvokeCodeGeneratorFactoryOnStandardServices(CodeGeneratorInformation codeGeneratorInformation)
            : base(codeGeneratorInformation)
        {
        }

        [Import]
        private IMsBuildProjectPropertyService MsBuildProjectPropertyService { get; set; }

        [Import]
        private VisualStudioWorkspace Workspace { get; set; }

        [Import("Microsoft.WebTools.Shared.IFileSystem")]
        private object FileSystem { get; set; }

        [Import(typeof(IProjectExportProvider))]
        private IProjectExportProvider ProjectExportProvider { get; set; }


        protected override IMvcCoreCodeGenerationActionService GetMvcCoreCodeGenerationActionService(CodeGenerationContext context)
        {
            var type = typeof(ICommandLineInvoker).Assembly
                .GetType("Microsoft.WebTools.Scaffolding.Core.MvcCoreCodeGenerationActionService");

            return (IMvcCoreCodeGenerationActionService)Activator.CreateInstance(type, context.ServiceProvider);
        }

        protected override IProjectContextBuilder GetProjectContextBuilder(CodeGenerationContext context)
        {
            var type = typeof(ICommandLineInvoker).Assembly
                .GetType("Microsoft.WebTools.Scaffolding.Core.ProjectModel.ProjectContextBuilder");

            var projectDependencyProvider =
                ProjectExportProvider.GetExport<IProjectDependencyProvider>(context.ActiveProject.FullName);

            return (IProjectContextBuilder)Activator.CreateInstance(type, projectDependencyProvider,
                context.ActiveProject, Workspace, context.ServiceProvider, MsBuildProjectPropertyService, FileSystem);
        }
    }
}
