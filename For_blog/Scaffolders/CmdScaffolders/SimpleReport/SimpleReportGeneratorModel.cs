using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;

namespace CmdScaffolders.SimpleReport
{
    public class SimpleReportGeneratorModel
    {
        [Argument(Description = "Name of report")]
        public string ReportName { get; set; }

        [Option(Name = "addTimeStamp", ShortName = "t", Description = "Add time stamp to report name" )]
        public bool AddTimeStamp { get; set; }
    }
}
