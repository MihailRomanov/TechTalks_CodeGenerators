using Generators.Common;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Generators.ModelReaders
{
    public static class AssemblyModelReader
    {
        public static Common.EntitiesModel.Entity[] Read(params Assembly[] assemblies)
        {
            var result = new List<Common.EntitiesModel.Entity>();

            foreach (var assembly in assemblies)
            {
                var markedTypes = assembly
                    .ExportedTypes
                    .Where(t => t.GetCustomAttribute<ModelSourceAttribute>() != null);

                foreach (var type in markedTypes)
                {
                    var properties = type
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Select(p => new Common.EntitiesModel.EntityProperty
                        {
                            Name = p.Name,
                            DataType = p.PropertyType.ToModelDataType()
                        });

                    var entity = new Common.EntitiesModel.Entity
                    {
                        Name = type.Name,
                        Properties = properties.ToArray()
                    };

                    result.Add(entity);
                }
            }

            return result.ToArray();
        }
    }
}
