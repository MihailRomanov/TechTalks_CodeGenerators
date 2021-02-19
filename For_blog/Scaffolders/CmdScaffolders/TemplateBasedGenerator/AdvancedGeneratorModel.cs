using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;

namespace CmdScaffolders.TemplateBasedGenerator
{
    public class AdvancedGeneratorModel
    {
        [Argument(Description = "Namespace")]
        public string Namespace { get; set; }

        [Option(Name = "typeFilter", ShortName = "tf", Description = "Regex for filter types for generator" )]
        public string TypeFilter { get; set; }

        [Option(Name = "outputFile", ShortName = "o", Description = "Result file Name", DefaultValue = "Generated.cs")]
        public string OutputFile { get; set; }
    }
}