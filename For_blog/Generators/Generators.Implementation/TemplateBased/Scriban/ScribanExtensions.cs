using Generators.Common.EntitiesModel;
using Scriban.Runtime;

namespace Generators.Implementation.TemplateBased.Scriban
{
    public class ScribanExtensions : ScriptObject
    {
        public static string ToNetType(DataType dataType)
        {
            return dataType.ToNetTypeString();
        }
    }
}
