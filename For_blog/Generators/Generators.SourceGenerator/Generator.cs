using Generators.ModelReaders;
using Microsoft.CodeAnalysis;
using System.IO;
using System.Linq;

namespace Generators.SourceGenerator
{
    [Generator]
    public class Generator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            var modelFiles = context.AdditionalFiles.Where(f => f.Path.EndsWith(".model.json"));
            var generator = new Implementation.TemplateBased.T4.T4Generator();

            context.AnalyzerConfigOptions.GlobalOptions
                .TryGetValue("build_property.RootNamespace", out var rootNamespace);
            context.AnalyzerConfigOptions.GlobalOptions
                .TryGetValue("build_property.MSBuildProjectDirectory", out var projectDirectory);

            foreach (var file in modelFiles)
            {
                var options = context.AnalyzerConfigOptions.GetOptions(file);
                options.TryGetValue("build_metadata.AdditionalFiles.CustomToolNamespace", out var namespaceName);

                var entities = JsonModelReader.Read(file.GetText().ToString());
                var model = new Common.EntitiesModel.Model
                {
                    Entities = entities,
                    NamespaceName = GetNamespace(namespaceName, file.Path, projectDirectory, rootNamespace)
                };

                var code = generator.Generate(model);

                context.AddSource(Path.GetFileName(file.Path), code);
            }
        }

        private string GetNamespace(string customToolNamespace, string filePath, string projectDir, string rootNamespace)
        {
            if (!string.IsNullOrEmpty(customToolNamespace))
                return customToolNamespace;

            var fileDir = Path.GetDirectoryName(Path.GetFullPath(filePath));
            var relativePath = fileDir.Replace(projectDir, "").Trim(Path.DirectorySeparatorChar);

            return (rootNamespace + "." + relativePath.Replace(Path.DirectorySeparatorChar, '.')).Trim('.');
        }
    }
}
