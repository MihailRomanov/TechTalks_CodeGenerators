using FluentAssertions;
using Generators.Common.EntitiesModel;
using NUnit.Framework;

namespace Generators.ModelReaders.Tests
{
    public partial class AssemblyModelReaderTests
    {

        [Test]
        public void ReadFromAssemblyTest()
        {
            var model = AssemblyModelReader.Read(this.GetType().Assembly);

            var entity = new Entity
            {
                Name = "AAAA",
                Properties = new[]
                {
                    new EntityProperty { Name = "BBB", DataType = DataType.String },
                    new EntityProperty { Name = "BBB2", DataType = DataType.Boolean },
                    new EntityProperty { Name = "BBB3", DataType = DataType.Integer },
                    new EntityProperty { Name = "BBB4", DataType = DataType.Real },
                }
            };

            model.Should().BeEquivalentTo(entity);
        }
    }
}