using FluentAssertions;
using Generators.Common.EntitiesModel;
using NUnit.Framework;

namespace Generators.ModelReaders.Tests
{
    public class JsonModelReaderTests
    {
        [Test]
        public void ReadJsonTest()
        {
            var jsonContent =
            "[{" +
                "\"name\": \"AAAA\"," +
                "\"properties\": [" +
                    "{ \"name\": \"BBB\", \"dataType\": \"String\" }," +
                    "{ \"name\": \"BBB2\", \"dataType\": \"Boolean\" }," +
                    "{ \"name\": \"BBB3\", \"dataType\": \"Integer\" }," +
                    "{ \"name\": \"BBB4\", \"dataType\": \"Real\" }" +
            "]}," +
            "{" +
                "\"name\": \"AAAA1\"" +
            "}]";

            var model = JsonModelReader.Read(jsonContent);

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