using FluentAssertions;
using Generators.Common.EntitiesModel;
using NUnit.Framework;

namespace Generators.ModelReaders.Tests
{
    public class XmlModelReaderTests
    {
        [Test]
        public void ReadXmlTest()
        {
            var xmlContent =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>"+
            "<Entities>" +
                "<Entity Name=\"AAAA\">" +
                    "<Property Name=\"BBB\" DataType=\"String\"/>" +
                    "<Property Name=\"BBB2\" DataType=\"Boolean\"/>" +
                    "<Property Name=\"BBB3\" DataType=\"Integer\"/>" +
                    "<Property Name=\"BBB4\" DataType=\"Real\"/>" +
                "</Entity>" +
                "<Entity Name=\"AAAA1\"></Entity>" +
            "</Entities>";

            var model = XmlModelReader.Read(xmlContent);

            var expectedModel = new[]
            {
                new Entity
                {
                    Name = "AAAA",
                    Properties = new[]
                    {
                        new EntityProperty { Name = "BBB", DataType = DataType.String },
                        new EntityProperty { Name = "BBB2", DataType = DataType.Boolean },
                        new EntityProperty { Name = "BBB3", DataType = DataType.Integer },
                        new EntityProperty { Name = "BBB4", DataType = DataType.Real },
                    },
                },
                new Entity { Name = "AAAA1", Properties = new EntityProperty[0] }
            };

            model.Should().BeEquivalentTo(expectedModel);
        }

    }
}