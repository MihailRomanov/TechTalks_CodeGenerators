using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.NuGet;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;
using Microsoft.WebTools.Scaffolding.Core;
using Microsoft.WebTools.Scaffolding.Core.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace VsScaffolders.CoreCall
{
    public abstract class BaseCoreCallCodeGenerator : CodeGenerator
    {
        protected readonly ICommandLineInvoker commandLineInvoker;
        protected readonly IFileSystemChangeExecutor fileSystemChangeExecutor;
        protected readonly IMvcCoreCodeGenerationActionService mvcCoreCodeGenerationActionService;
        protected readonly Workspace workspace;
        protected readonly IMsBuildProjectPropertyService msBuildProjectPropertyService;
        protected readonly IVsSolution vsSolution;

        protected abstract string CodeGeneratorPackageName { get; }
        protected abstract string CodeGeneratorPackageVersion { get; }

        public BaseCoreCallCodeGenerator(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {
            commandLineInvoker = context.Items.GetProperty<ICommandLineInvoker>(typeof(ICommandLineInvoker));
            workspace = context.Items.GetProperty<Workspace>(typeof(Workspace));
            msBuildProjectPropertyService = context.Items.GetProperty<IMsBuildProjectPropertyService>(typeof(IMsBuildProjectPropertyService));
            fileSystemChangeExecutor = context.Items.GetProperty<IFileSystemChangeExecutor>(typeof(IFileSystemChangeExecutor));
            mvcCoreCodeGenerationActionService = context.Items.GetProperty<IMvcCoreCodeGenerationActionService>(typeof(IMvcCoreCodeGenerationActionService));

            vsSolution = (IVsSolution)ServiceProvider.GetService(typeof(IVsSolution));
        }

        public override IEnumerable<NuGetPackage> Dependencies => new List<NuGetPackage>
        {
            new NuGetPackage(CodeGeneratorPackageName, CodeGeneratorPackageVersion, new NuGetSourceRepository()),
            new NuGetPackage("Microsoft.VisualStudio.Web.CodeGeneration.Design", "5.0.*", new NuGetSourceRepository())
        };

        private IProjectContext GetProjectContext()
        {
            var type = typeof(ICommandLineInvoker).Assembly
                .GetType("Microsoft.WebTools.Scaffolding.Core.Scaffolders.ProjectPreparationContext");
            var instance = Activator.CreateInstance(type, Context, Context.ServiceProvider);

            MethodInfo theMethod = type.GetMethod("GetProjectContext");
            var projectContext = (IProjectContext)theMethod.Invoke(
                instance, new object[] { workspace, msBuildProjectPropertyService });

            var pc = (CommonProjectContext)projectContext;
            var p1 = pc.PackageDependencies.Single(p => p.Name == CodeGeneratorPackageName && p.Version == CodeGeneratorPackageVersion);
            p1.AddDependency(new Dependency("Microsoft.VisualStudio.Web.CodeGeneration", ""));

            return projectContext;
        }

        private IVsHierarchy GetVsHierarchyForSolution()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            IVsHierarchy ivsHierarchy;
            vsSolution.GetProjectOfUniqueName(Context.ActiveProject.UniqueName, out ivsHierarchy);

            return ivsHierarchy;
        }

        protected void InvokeCoreGenerator(string commandLine)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var uniqueName = Context.ActiveProject.UniqueName;
            var solutionBuild = Context.ActiveProject.DTE.Solution.SolutionBuild;
            //var context = solutionBuild.ActiveConfiguration.SolutionContexts
            //    .OfType<SolutionContext>().Single(c => c.ProjectName == uniqueName);
            //context.ShouldBuild = true;
            solutionBuild.Clean(true);
            solutionBuild.BuildProject(solutionBuild.ActiveConfiguration.Name, uniqueName, true);

            var projectContext = GetProjectContext();
            commandLineInvoker.Invoke(Context.ActiveProject, projectContext, commandLine);

            var solutionHierarchy = GetVsHierarchyForSolution();

            ThreadHelper.JoinableTaskFactory.Run(async delegate
            {
                await fileSystemChangeExecutor.ExecuteChanges(
                    solutionHierarchy, commandLineInvoker.ChangeStore, mvcCoreCodeGenerationActionService);
            });
        }
    }
}
