using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Collections.Generic;
using System.IO;

namespace Generators
{
    public abstract class BaseGeneratorTask: Task
    {
        public ITaskItem[] ModelFiles { get; set; }
        public string OutFolder { get; set; }
        public string RootNamespace { get; set; }


        [Output]
        public ITaskItem[] ResultFiles { get; set; }

        protected abstract string Generate(string modelContent, string namespaceName);
        protected abstract string OutFileExtension { get; }

        public override bool Execute()
        {
            var resultFiles = new List<TaskItem>();

            if (ModelFiles == null || ModelFiles.Length == 0) return true;

            foreach (var infileItem in ModelFiles)
            {
                var fileName = Path.GetFileName(infileItem.ItemSpec);
                var relativePath = infileItem.GetMetadata("RelativeDir");
                var outFileName = Path.Combine(OutFolder, relativePath, fileName + OutFileExtension);
                var customToolNamespace = infileItem.GetMetadata("CustomToolNamespace");
                var namespaceName = GetNamespace(customToolNamespace, relativePath, RootNamespace);

                var outFileFolder = Path.GetDirectoryName(outFileName);
                if (!Directory.Exists(outFileFolder))
                    Directory.CreateDirectory(outFileFolder);

                using (var inFile = new StreamReader(infileItem.ItemSpec, true))
                using (var outFile = new StreamWriter(outFileName))
                {
                    var inputText = inFile.ReadToEnd();


                    var result = Generate(inputText, namespaceName);

                    outFile.WriteLine(result);
                    outFile.Close();
                }

                resultFiles.Add(new TaskItem(outFileName));
            }

            ResultFiles = resultFiles.ToArray();

            return true;
        }

        private string GetNamespace(string customToolNamespace, string relativePath, string rootNamespace)
        {
            if (!string.IsNullOrEmpty(customToolNamespace))
                return customToolNamespace;

            return $"{rootNamespace}.{relativePath.Replace(Path.DirectorySeparatorChar, '.')}".TrimEnd('.');

        }
    }
}

