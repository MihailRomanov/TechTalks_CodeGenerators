using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;
using Microsoft.WebTools.Scaffolding.Core;
using Microsoft.WebTools.Scaffolding.Shared.ProjectModel;
using NuGet.VisualStudio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VsScaffolders.Helpers;

namespace VsScaffolders.CoreInvoke
{
    public class ProjectContextBuilder : IProjectContextBuilder
    {
        private readonly IMsBuildProjectPropertyService msBuildProjectPropertyService;
        private readonly EnvDTE.Project project;
        private readonly IServiceProvider serviceProvider;
        private readonly Workspace workspace;
        private readonly IVsPackageInstallerServices vsPackageInstaller;

        public ProjectContextBuilder(IMsBuildProjectPropertyService msBuildProjectPropertyService,
            EnvDTE.Project project, IServiceProvider serviceProvider, Workspace workspace,
            IVsPackageInstallerServices vsPackageInstaller)
        {
            this.msBuildProjectPropertyService = msBuildProjectPropertyService;
            this.project = project;
            this.serviceProvider = serviceProvider;
            this.workspace = workspace;
            this.vsPackageInstaller = vsPackageInstaller;
        }

        public async Task<IProjectContext> BuildProjectContext()
        {
            var context = new CommonProjectContext();

            var configuration = project.ConfigurationManager.ActiveConfiguration.ConfigurationName;
            var vsHierarchy = VsHierarchyHelper.GetProjectHierarchy(serviceProvider, project);
            var targetPath = msBuildProjectPropertyService.GetPropertyValue(vsHierarchy, "TargetPath", configuration);
            var outputType = msBuildProjectPropertyService.GetPropertyValue(vsHierarchy, "OutputType", configuration);
            var targetFramework = msBuildProjectPropertyService.GetPropertyValue(vsHierarchy, "TargetFramework", configuration);

            context.AssemblyFullPath = targetPath;
            context.AssemblyName = Path.GetFileName(targetPath);
            context.CompilationAssemblies = GetReferencedAssemblies();
            context.CompilationItems = GetCompilationItems();
            context.Config = targetPath + ".config";
            context.Configuration = configuration;
            context.DepsFile = msBuildProjectPropertyService.GetPropertyValue(vsHierarchy, "ProjectDepsFileName", configuration);
            context.EmbededItems = Enumerable.Empty<string>();
            context.IsClassLibrary = "Library".Equals(outputType, StringComparison.OrdinalIgnoreCase);
            context.PackageDependencies = GetPackageDependencies(targetFramework);
            context.PackagesDirectory = "";
            context.Platform = project.ConfigurationManager.ActiveConfiguration.PlatformName;
            context.ProjectFullPath = project.FileName;
            context.ProjectName = project.Name;
            context.ProjectReferenceInformation = GetProjectReferenceInformation();
            context.ProjectReferences = context.ProjectReferenceInformation.Select(pi => pi.FullPath).ToList();
            context.RootNamespace = msBuildProjectPropertyService.GetPropertyValue(vsHierarchy, "RootNamespace", configuration);
            context.RuntimeConfig = msBuildProjectPropertyService.GetPropertyValue(vsHierarchy, "ProjectRuntimeConfigFileName", configuration);
            context.TargetDirectory = msBuildProjectPropertyService.GetPropertyValue(vsHierarchy, "TargetDir", configuration);
            context.TargetFramework = targetFramework;

            return context;
        }

        private IEnumerable<ProjectReferenceInformation> GetProjectReferenceInformation()
        {
            var result = new List<ProjectReferenceInformation>();

            var workspaceProject = workspace.CurrentSolution.Projects
                .Single(p => p.FilePath.Equals(project.FileName, StringComparison.OrdinalIgnoreCase));

            var dependetProjectIds = workspace.CurrentSolution.GetProjectDependencyGraph()
                .GetProjectsThatThisProjectTransitivelyDependsOn(workspaceProject.Id);

            foreach (var pId in dependetProjectIds)
            {
                var project = workspace.CurrentSolution.GetProject(pId);

                result.Add(new ProjectReferenceInformation
                {
                    AssemblyName = project.AssemblyName,
                    CompilationItems = project.Documents.Select(d => d.FilePath),
                    FullPath = project.FilePath,
                    ProjectName = project.Name
                });
            }

            return result;
        }

        private IEnumerable<DependencyDescription> GetPackageDependencies(string targetFramework)
        {
            return vsPackageInstaller.GetInstalledPackages(project)
                .Select(p => new DependencyDescription(p.Id, p.VersionString, p.InstallPath, targetFramework, DependencyType.Package, true))
                .ToList();
        }

        private IEnumerable<string> GetCompilationItems()
        {
            var workspaceProject = workspace.CurrentSolution.Projects
                .Single(p => p.FilePath.Equals(project.FileName, StringComparison.OrdinalIgnoreCase));

            return workspaceProject.Documents.Select(d => d.FilePath).ToList();
        }

        private IEnumerable<ResolvedReference> GetReferencedAssemblies()
        {
            var vsProject = (VSLangProj.VSProject)project.Object;
            return vsProject.References.OfType<VSLangProj.Reference>().Select(r => new ResolvedReference(r.Name, r.Path)).ToList();
        }

        public IProjectContextBuilder ForTargetFramework(string targetFramework)
        {
            throw new NotImplementedException();
        }
    }
}
