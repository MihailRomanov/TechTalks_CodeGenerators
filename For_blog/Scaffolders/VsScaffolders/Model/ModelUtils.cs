using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using System.Linq;

namespace VsScaffolders.Model
{
    public static class ModelUtils
    {
        public static CodeTypeModel GetCodeTypeModel(CodeType codeType, IReflectedTypesService reflectedTypesService)
        {
            var prj = codeType.ProjectItem.ContainingProject;
            var name = codeType.Name;
            var properties = codeType.GetPublicMembers().OfType<CodeProperty>()
                .Select(p => new CodePropertyModel(p.Name, reflectedTypesService.GetType(prj, p.Type.AsFullName)))
                .ToArray();

            return new CodeTypeModel(name, properties);
        }

        public static CodeTypeModel[] GetCodeTypeModels(Project project,
            ICodeTypeService codeTypeService, IReflectedTypesService reflectedTypesService)
        {
            var types = codeTypeService.GetAllCodeTypes(project).ToArray();
            var models = types.Select(t => ModelUtils.GetCodeTypeModel(t, reflectedTypesService)).ToArray();

            return models;
        }
    }
}
