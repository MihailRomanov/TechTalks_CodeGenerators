using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;
using Microsoft.VisualStudio.Web.CodeGeneration.DotNet;
using System;
using System.IO;
using System.Text;

namespace CmdScaffolders.SimpleReport
{
    [Alias("report")]
    public class SimpleReportGenerator : ICodeGenerator
    {
        private readonly IApplicationInfo applicationInfo;
        private readonly IProjectContext projectContext;
        private readonly IModelTypesLocator modelTypesLocator;
        private readonly IFileSystem fileSystem;
        private readonly ILogger logger;

        public SimpleReportGenerator(
            IApplicationInfo applicationInfo,
            IProjectContext projectContext,
            IModelTypesLocator modelTypesLocator,
            IFileSystem fileSystem,
            ILogger logger)
        {
            this.applicationInfo = applicationInfo;
            this.projectContext = projectContext;
            this.modelTypesLocator = modelTypesLocator;
            this.fileSystem = fileSystem;
            this.logger = logger;
        }

        public void GenerateCode(SimpleReportGeneratorModel model)
        {
            var reportName = string.IsNullOrEmpty(model.ReportName) ? "Report" : model.ReportName;

            if (model.AddTimeStamp)
                reportName += DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

            reportName += ".rpt";

            var reportContent = CreateReport();

            fileSystem.WriteAllText(Path.Combine(applicationInfo.ApplicationBasePath, reportName), reportContent);
        }

        private string CreateReport()
        {
            var result = new StringBuilder();

            result.AppendLine($"Application: {applicationInfo.ApplicationName}");

            logger.LogMessage("Enumerate project files", LogMessageLevel.Trace);
            result.AppendLine("Files");

            foreach (var file in fileSystem.EnumerateFiles(
                applicationInfo.ApplicationBasePath, "*.*", SearchOption.AllDirectories))
            {
                result.AppendLine($"\t{file}");
            }

            logger.LogMessage("Enumerate project types", LogMessageLevel.Trace);
            result.AppendLine("Types");
            foreach (var type in modelTypesLocator.GetAllTypes())
            {
                result.AppendLine($"\t{type.FullName}");
            }


            return result.ToString();
        }
    }
}
