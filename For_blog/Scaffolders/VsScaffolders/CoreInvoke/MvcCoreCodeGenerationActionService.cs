using Microsoft.VisualStudio.ProjectSystem;
using Microsoft.WebTools.Scaffolding.Core;
using System.Threading.Tasks;
using Microsoft.AspNet.Scaffolding;
using System.IO;
using System.Text;

namespace VsScaffolders.CoreInvoke
{
    public class MvcCoreCodeGenerationActionService : IMvcCoreCodeGenerationActionService
    {
        private readonly IFileSystemService fileSystemService;

        public MvcCoreCodeGenerationActionService(IFileSystemService fileSystemService)
        {
            this.fileSystemService = fileSystemService;
        }

        public Task<bool> AddFileAsync(UnconfiguredProject project, string projectRelativePath,
            string sourceFilePath, bool skipIfExists, bool addUTF8BOM = true)
        {
            var text = fileSystemService.ReadAllText(sourceFilePath);

            return AddFileFromTextAsync(project, projectRelativePath, text, skipIfExists, addUTF8BOM);
        }

        public async Task<bool> AddFileFromTextAsync(UnconfiguredProject project, string projectRelativePath,
            string sourceText, bool skipIfExists, bool addUTF8BOM = true)
        {
            var projectRootDirectory = Path.GetDirectoryName(project.FullPath);
            var fullFilePath = Path.Combine(projectRootDirectory, projectRelativePath);

            var fileExist = fileSystemService.FileExists(fullFilePath);

            if (fileExist && skipIfExists)
                return false;

            fileSystemService.WriteAllText(fullFilePath, sourceText, new UTF8Encoding(addUTF8BOM));

            var configuredProject = project.Services.ActiveConfiguredProjectProvider.ActiveConfiguredProject;
            var inProjectPath = PathHelper.MakeRelative(projectRootDirectory, fullFilePath);

            await configuredProject.Services.SourceItems.AddAsync(inProjectPath);

            return true;
        }

        public async Task<bool> AddFolderAsync(UnconfiguredProject project, string projectRelativePath)
        {
            var result = false;
            var projectRootDirectory = Path.GetDirectoryName(project.FullPath);
            var fullDirectoryPath = Path.Combine(projectRootDirectory, projectRelativePath);

            if (!fileSystemService.DirectoryExists(fullDirectoryPath))
            {
                fileSystemService.CreateDirectory(fullDirectoryPath);
                result = true;
            }

            var configuredProject = project.Services.ActiveConfiguredProjectProvider.ActiveConfiguredProject;
            var inProjectPath = PathHelper.MakeRelative(projectRootDirectory, fullDirectoryPath);

            await configuredProject.Services.SourceItems.AddAsync(ExportContractNames.ProjectItemProviders.Folders, inProjectPath);

            return result;
        }
    }
}
