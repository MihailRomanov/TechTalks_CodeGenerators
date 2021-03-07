using EnvDTE;
using Microsoft.AspNet.Scaffolding;
using System.Linq;

namespace VsScaffolders.Model
{
    public class CodeTypeModel
    {
        public string Name { get; }
        public CodePropertyModel[] Properties { get; }

        public CodeTypeModel(string name, CodePropertyModel[] properties)
        {
            Name = name;
            Properties = properties;
        }


        public static CodeTypeModel FromCodeType(CodeType codeType, IReflectedTypesService reflectedTypesService)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            var prj = codeType.ProjectItem.ContainingProject;
            var name = codeType.Name;
            var properties = codeType.GetPublicMembers().OfType<CodeProperty>()
                .Select(p => new CodePropertyModel(p.Name, reflectedTypesService.GetType(prj, p.Type.AsFullName)))
                .ToArray();

            return new CodeTypeModel(name, properties);
        }
    }
}
