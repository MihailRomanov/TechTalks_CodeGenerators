using Microsoft.AspNet.Scaffolding;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;
using Microsoft.WebTools.Scaffolding.Core;
using Microsoft.WebTools.Scaffolding.Core.CommandLine;
using Microsoft.WebTools.Scaffolding.Shared.ProjectModel;
using System.Linq;
using System.Windows;

namespace VsScaffolders.CoreInvoke
{
    public class CoreInvokeCodeGenerator : CodeGenerator
    {
        protected readonly ICommandLineInvoker commandLineInvoker;
        protected readonly IFileSystemChangeExecutor fileSystemChangeExecutor;
        protected readonly IProjectContextBuilder projectContextBuilder;
        protected readonly IMvcCoreCodeGenerationActionService mvcCoreCodeGenerationActionService;

        protected string CodeGeneratorPackageName => "CmdScaffolders";

        protected string CodeGeneratorPackageVersion => "1.0.0";

        public CoreInvokeCodeGenerator(CodeGenerationContext context, CodeGeneratorInformation information)
            : base(context, information)
        {
            commandLineInvoker = context.Items.GetProperty<ICommandLineInvoker>(typeof(ICommandLineInvoker));
            fileSystemChangeExecutor = context.Items.GetProperty<IFileSystemChangeExecutor>(typeof(IFileSystemChangeExecutor));
            mvcCoreCodeGenerationActionService = context.Items.GetProperty<IMvcCoreCodeGenerationActionService>(typeof(IMvcCoreCodeGenerationActionService));
            projectContextBuilder = context.Items.GetProperty<IProjectContextBuilder>(typeof(IProjectContextBuilder));
        }

        public override void GenerateCode()
        {
            //MessageBox.Show("Generate!!!");
            InvokeCoreGenerator("report MyReport -t");
        }

        public override bool ShowUIAndValidate()
        {
            return MessageDialog.Show("Coninue?", "Continue", MessageDialogCommandSet.YesNo) == MessageDialogCommand.Yes;
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

            //var projectContext = projectContextBuilder.BuildProjectContext();

            var projectContext = ThreadHelper.JoinableTaskFactory.Run(() => projectContextBuilder.BuildProjectContext());

            var p1 = projectContext.PackageDependencies.Single(p => p.Name == CodeGeneratorPackageName
                && p.Version == CodeGeneratorPackageVersion);
            p1.AddDependency(new Dependency("Microsoft.VisualStudio.Web.CodeGeneration", ""));

            commandLineInvoker.Invoke(Context.ActiveProject, projectContext, commandLine);

            var solutionHierarchy = Helpers.VsHierarchyHelper.GetProjectHierarchy(Context.ServiceProvider, Context.ActiveProject);

            ThreadHelper.JoinableTaskFactory.Run(async delegate
            {
                await fileSystemChangeExecutor.ExecuteChanges(
                    solutionHierarchy, commandLineInvoker.ChangeStore, mvcCoreCodeGenerationActionService);
            });
        }
    }
}
