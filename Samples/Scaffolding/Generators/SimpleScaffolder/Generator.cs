using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;

namespace SimpleScaffolder
{
    [Alias("simple")]
    public class SimpleCodeGenerator : ICodeGenerator
    {
        public SimpleCodeGenerator()
        {
        }

        public async Task GenerateCode(SimpleCodeGeneratorModel model)
        {
            var name = Path.GetFileNameWithoutExtension(model.FileName);
            var ext = Path.GetExtension(model.FileName);

            for (int i = 0; i < int.Parse(model.Count); i++)
            {
                var fileName = name + i.ToString() + ext;
                File.WriteAllText(Path.GetFullPath(fileName), "!!!");
            }
        }
    }

    public class SimpleCodeGeneratorModel
    {
        [Argument(Description = "File name")]
        public string FileName { get; set; }

        [Option(Name = "count", ShortName = "c", Description = "Files count", DefaultValue = "1")]
        public string Count { get; set; }
    }
}
