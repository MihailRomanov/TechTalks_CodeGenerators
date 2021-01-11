using Generators.Common.EntitiesModel;

namespace Generators.Implementation.TemplateBased.T4
{
    public partial class CSharpTemplate : CSharpTemplateBase
    {
        public CSharpTemplate(Model model)
        {
            _ModelField = model;
        }
    }
}