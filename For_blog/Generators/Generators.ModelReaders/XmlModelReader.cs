using Generators.ModelReaders.ReadingModel;
using System.IO;
using System.Xml.Serialization;

namespace Generators.ModelReaders
{
    public class XmlModelReader
    {
        public static Common.EntitiesModel.Entity[] Read(string xmlContent)
        {
            var serializer = new XmlSerializer(typeof(EntityArray));
            var reader = new StringReader(xmlContent);

            return ((EntityArray)serializer.Deserialize(reader)).ToArray().ToEntitiesModel();
        }
    }
}
