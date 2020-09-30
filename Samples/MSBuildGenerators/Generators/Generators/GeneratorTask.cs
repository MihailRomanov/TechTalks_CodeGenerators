using EntityModel;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Collections.Generic;
using System.IO;

namespace Generators
{
    public class GeneratorTask : Task
    {
        public ITaskItem[] ModelFiles { get; set; }
        public string OutFolder { get; set; }
        public string Namespace { get; set; }


        [Output]
        public ITaskItem[] ResultFiles { get; set; }


        public override bool Execute()
        {
            var resultFiles = new List<TaskItem>();

            if (ModelFiles == null || ModelFiles.Length == 0) return true;

            foreach (var infileItem in ModelFiles)
            {
                var fileName = Path.GetFileName(infileItem.ItemSpec);
                var outFileName = Path.Combine(OutFolder, fileName + ".cs");

                using (var inFile = new StreamReader(infileItem.ItemSpec, true))
                using (var outFile = new StreamWriter(outFileName))
                {
                    var inputText = inFile.ReadToEnd();
                    var model = ModelHelper.ReadModel(inputText);
                    var template = new CSharpTemplate(model, Namespace);

                    outFile.WriteLine(template.TransformText());
                    outFile.Close();
                }

                resultFiles.Add(new TaskItem { ItemSpec = outFileName });
            }

            ResultFiles = resultFiles.ToArray();

            return true;

        }
    }
}
