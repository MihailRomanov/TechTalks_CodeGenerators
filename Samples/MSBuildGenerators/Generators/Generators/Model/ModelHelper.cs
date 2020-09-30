using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace EntityModel
{
	public static class ModelHelper
	{
		public static Entity ReadModel(string modelString)
		{
			return ReadModel(new StringReader(modelString));
		}

		public static Entity ReadModel(TextReader reader)
		{
			var serializer = new XmlSerializer(typeof(Entity));
			var xmlReader = XmlReader.Create(reader);
			return (Entity)serializer.Deserialize(xmlReader);
		}
	}
}
