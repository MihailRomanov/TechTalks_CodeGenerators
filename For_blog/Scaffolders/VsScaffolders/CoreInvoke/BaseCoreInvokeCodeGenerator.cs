using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.NuGet;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;
using Microsoft.WebTools.Scaffolding.Core;
using Microsoft.WebTools.Scaffolding.Core.CommandLine;
using Microsoft.WebTools.Scaffolding.Shared.ProjectModel;
using System.Collections.Generic;
using System.Linq;
using VsScaffolders.Helpers;

namespace VsScaffolders.CoreInvoke
{
    public abstract class BaseCoreInvokeCodeGenerator : CodeGenerator
    {
        private const string CodeGenerationDesignPackageId = "Microsoft.VisualStudio.Web.CodeGeneration.Design";
        private const string CodeGenerationDesignPackageVersion = "5.0.*";
        private const string CodeGenerationPackageId = "Microsoft.VisualStudio.Web.CodeGeneration";

        protected readonly ICommandLineInvoker commandLineInvoker;
        protected readonly IFileSystemChangeExecutor fileSystemChangeExecutor;
        protected readonly IMvcCoreCodeGenerationActionService mvcCoreCodeGenerationActionService;
        protected readonly IProjectContextBuilder projectContextBuilder;

        protected abstract string CodeGeneratorPackageName { get; }
        protected abstract string CodeGeneratorPackageVersion { get; }

        public BaseCoreInvokeCodeGenerator(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {
            commandLineInvoker = context.Items.GetProperty<ICommandLineInvoker>(typeof(ICommandLineInvoker));
            fileSystemChangeExecutor = context.Items.GetProperty<IFileSystemChangeExecutor>(typeof(IFileSystemChangeExecutor));
            mvcCoreCodeGenerationActionService = context.Items.GetProperty<IMvcCoreCodeGenerationActionService>(typeof(IMvcCoreCodeGenerationActionService));
            projectContextBuilder = context.Items.GetProperty<IProjectContextBuilder>(typeof(IProjectContextBuilder));
        }

        public override IEnumerable<NuGetPackage> Dependencies => new List<NuGetPackage>
        {
            new NuGetPackage(CodeGeneratorPackageName, CodeGeneratorPackageVersion, new NuGetSourceRepository()),
            new NuGetPackage(CodeGenerationDesignPackageId, CodeGenerationDesignPackageVersion, new NuGetSourceRepository())
        };

        private IProjectContext GetProjectContext()
        {
            var projectContext = ThreadHelper.JoinableTaskFactory.Run(
                async delegate { return await projectContextBuilder.BuildProjectContext(); });

            var currentPackageDependency = projectContext.PackageDependencies
                .Single(p => p.Name == CodeGeneratorPackageName && p.Version == CodeGeneratorPackageVersion);
            currentPackageDependency.AddDependency(new Dependency(CodeGenerationPackageId, ""));

            return projectContext;
        }

        protected void InvokeCoreGenerator(string commandLine)
        {
            RebuildProject(Context.ActiveProject);

            var projectContext = GetProjectContext();
            commandLineInvoker.Invoke(Context.ActiveProject, projectContext, commandLine);

            var solutionHierarchy = VsHierarchyHelper.GetProjectHierarchy(Context.ServiceProvider, Context.ActiveProject);

            ThreadHelper.JoinableTaskFactory.Run(async delegate
            {
                await fileSystemChangeExecutor.ExecuteChanges(
                    solutionHierarchy, commandLineInvoker.ChangeStore, mvcCoreCodeGenerationActionService);
            });
        }

        private void RebuildProject(Project project)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var uniqueName = project.UniqueName;
            var solutionBuild = project.DTE.Solution.SolutionBuild;
            solutionBuild.Clean(true);
            solutionBuild.BuildProject(solutionBuild.ActiveConfiguration.Name, uniqueName, true);
        }
    }
}
