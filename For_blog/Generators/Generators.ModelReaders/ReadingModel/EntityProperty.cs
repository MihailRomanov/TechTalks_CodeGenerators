using System.Xml.Serialization;

namespace Generators.ModelReaders.ReadingModel
{
    public class EntityProperty
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public DataType DataType { get; set; }
    }
}