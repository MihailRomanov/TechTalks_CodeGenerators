using Microsoft.CSharp;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;

namespace CodeDomSimple
{
    public static class Generator
    {
        public static string namespaceName = "A";
        public static string className = "AAA";
        public static string propName = "AAAAA";

        public static string Generate(int count)
        {
            var @namespace = new CodeNamespace(namespaceName);

            for (var i = 0; i < count; i++)
            {
                var @class = new CodeTypeDeclaration(className)
                {
                    IsClass = true
                };

                var fieldName = "_" + propName;
                var type = typeof(string);

                var field = new CodeMemberField(type, fieldName);

                var property = new CodeMemberProperty { Name = propName  };
                property.Type = new CodeTypeReference(type);

                property.GetStatements.Add(
                    new CodeMethodReturnStatement(
                        new CodeFieldReferenceExpression(
                            new CodeThisReferenceExpression(), fieldName)));

                property.SetStatements.Add(
                    new CodeAssignStatement(
                        new CodeFieldReferenceExpression(
                            new CodeThisReferenceExpression(), fieldName),
                        new CodePropertySetValueReferenceExpression())
                    );

                @class.Members.Add(field);
                @class.Members.Add(property);

                @namespace.Types.Add(@class);
            }

            var codeUnit = new CodeCompileUnit();
            codeUnit.Namespaces.Add(@namespace);

            var csProvider = new CSharpCodeProvider();
            var result = new StringWriter();
            csProvider.GenerateCodeFromCompileUnit(codeUnit, result, new CodeGeneratorOptions());

            return result.ToString();
        }
    }
}
