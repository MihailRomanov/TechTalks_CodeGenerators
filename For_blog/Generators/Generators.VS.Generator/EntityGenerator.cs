using Generators.Common.EntitiesModel;
using Generators.Implementation.TemplateBased.T4;
using Generators.ModelReaders;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using System.Runtime.InteropServices;
using System.Text;

namespace Generators.VS.Generator
{
    [Guid("132DFB47-2ED7-45BF-BC07-C65003E7F088")]
    [ProvideCodeGenerator(
        typeof(EntityGenerator),
        "EntityGenerator",
        "Entity DTO Generator",
        true)]
    public class EntityGenerator : BaseCodeGenerator
    {
        public override string GetDefaultExtension() => ".cs";

        protected override byte[] GenerateCode(string inputFileName, string inputFileContent)
        {
            var entities = JsonModelReader.Read(inputFileContent);
            var model = new Model
            {
                Entities = entities,
                NamespaceName = FileNamespace
            };

            var generator = new T4Generator();
            var outContent = generator.Generate(model);

            return Encoding.UTF8.GetBytes(outContent);
        }
    }
}
