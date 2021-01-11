using Generators.Common;
using Generators.Common.EntitiesModel;
using RazorLight;
using RazorLight.Razor;

namespace Generators.Implementation.TemplateBased.Razor
{
    public class RazorGenerator : IGenerator<Model>
    {
        private readonly RazorLightEngine engine;
        private const string TemplateName =
            "Generators.Implementation.TemplateBased.Razor.Template.cshtml";

        public RazorGenerator()
        {
            engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(GetType().Assembly)
                .UseMemoryCachingProvider()
                .Build();

            engine.CompileTemplateAsync(TemplateName).Wait();
        }

        public string Generate(Model model)
        {
            var template = engine.Handler.Cache.RetrieveTemplate(TemplateName)
                .Template.TemplatePageFactory();
            return engine.RenderTemplateAsync(template, model).Result;
        }
    }
}
