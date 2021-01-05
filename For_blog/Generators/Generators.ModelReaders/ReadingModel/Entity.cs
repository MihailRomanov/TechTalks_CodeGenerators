using System.Xml.Serialization;

namespace Generators.ModelReaders.ReadingModel
{
    public class Entity
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlElement("Property")]
        public EntityProperty[] Properties { get; set; }
    }
}