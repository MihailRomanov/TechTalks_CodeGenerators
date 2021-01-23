using Generators.Common.EntitiesModel;
using Generators.Implementation.TemplateBased.T4;
using Generators.ModelReaders;

namespace Generators.Build
{
    public class EntityGeneratorTask : BaseGeneratorTask
    {
        protected override string OutFileExtension => ".cs";

        protected override string Generate(string modelContent, string namspaceName)
        {
            var model = JsonModelReader.Read(modelContent);
            var fullModel = new Model
            {
                Entities = model,
                NamespaceName = namspaceName
            };
            var generator = new T4Generator();
            return generator.Generate(fullModel);
        }
    }
}
