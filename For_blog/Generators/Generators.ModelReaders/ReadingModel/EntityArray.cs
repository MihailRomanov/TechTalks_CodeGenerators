using System.Collections.Generic;
using System.Xml.Serialization;

namespace Generators.ModelReaders.ReadingModel
{

    [XmlRoot("Entities")]
    public class EntityArray: List<Entity>{}
}
