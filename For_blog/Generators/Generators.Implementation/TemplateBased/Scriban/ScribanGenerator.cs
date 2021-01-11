using Generators.Common;
using Generators.Common.EntitiesModel;
using Scriban;
using Scriban.Runtime;
using System.IO;

namespace Generators.Implementation.TemplateBased.Scriban
{
    public class ScribanGenerator : IGenerator<Model>
    {
        private const string TemplateName =
            "Generators.Implementation.TemplateBased.Scriban.Template.liquid";

        private readonly Template template;

        public ScribanGenerator()
        {
            var templateText = GetTemplateText(TemplateName);
            template = Template.Parse(templateText);
        }

        public string Generate(Model model)
        {
            var scriptObject = new ScribanExtensions();
            scriptObject.Import(model);
            var context = new TemplateContext(scriptObject);

            return template.Render(context);
        }

        private string GetTemplateText(string templateName)
        {
            using (var stream = GetType().Assembly.GetManifestResourceStream(templateName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
