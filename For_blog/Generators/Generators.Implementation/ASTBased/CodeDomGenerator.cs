using Generators.Common;
using Generators.Common.EntitiesModel;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;

namespace Generators.Implementation.ASTBased
{
    public class CodeDomGenerator : IGenerator<Model>
    {
        private readonly CodeDomProvider provider;

        public CodeDomGenerator()
            : this("CSharp")
        { }

        public CodeDomGenerator(string lang)
            : this(CodeDomProvider.CreateProvider(lang))
        { }

        public CodeDomGenerator(CodeDomProvider provider)
        {
            this.provider = provider;
        }

        public string Generate(Model model)
        {
            var codeUnit = GetCodeCompileUnit(model.Entities, model.NamespaceName);
            var result = new StringWriter();
            provider.GenerateCodeFromCompileUnit(codeUnit, result, new CodeGeneratorOptions());

            return result.ToString();
        }

        private CodeCompileUnit GetCodeCompileUnit(Entity[] entities, string namespaceName)
        {
            var @namespace = new CodeNamespace(namespaceName);

            foreach (var entity in entities)
            {

                var @class = new CodeTypeDeclaration
                {
                    Name = entity.Name,
                    IsClass = true
                };

                @namespace.Types.Add(@class);

                foreach (var prop in entity.Properties ?? Enumerable.Empty<EntityProperty>())
                {
                    var fieldName = "_" + prop.Name;
                    var type = prop.DataType.ToNetType();

                    var field = new CodeMemberField(type, fieldName)
                    {
                        Attributes = MemberAttributes.Private
                    };

                    var property = new CodeMemberProperty
                    {
                        Name = prop.Name,
                        Attributes = MemberAttributes.Public,
                        Type = new CodeTypeReference(type)
                    };

                    property.GetStatements.Add(
                        new CodeMethodReturnStatement(
                            new CodeFieldReferenceExpression(
                                new CodeThisReferenceExpression(), fieldName)));

                    property.SetStatements.Add(
                        new CodeAssignStatement(
                            new CodeFieldReferenceExpression(
                                new CodeThisReferenceExpression(), fieldName),
                            new CodeVariableReferenceExpression() { VariableName = "value" })
                        );

                    @class.Members.Add(field);
                    @class.Members.Add(property);
                }
            }

            var codeUnit = new CodeCompileUnit();
            codeUnit.Namespaces.Add(@namespace);

            return codeUnit;
        }
    }
}
