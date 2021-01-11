using Generators.Common;
using Generators.Common.EntitiesModel;

namespace Generators.Implementation.TemplateBased.T4
{
    public class T4Generator : IGenerator<Model>
    {
        public string Generate(Model model)
        {
            var template = new CSharpTemplate(model);
            return template.TransformText();
        }
    }
}
