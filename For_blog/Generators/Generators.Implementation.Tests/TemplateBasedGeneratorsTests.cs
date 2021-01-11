using Generators.Common.EntitiesModel;
using Generators.Implementation.TemplateBased.Razor;
using Generators.Implementation.TemplateBased.Scriban;
using Generators.Implementation.TemplateBased.T4;
using NUnit.Framework;
using System;

namespace Generators.Implementation.Tests
{
    public class TemplateBasedGeneratorsTests
    {
        readonly Model model = new Model
        {
            NamespaceName = "ResultNamespace",
            Entities = new[]
            {
                new Entity
                {
                    Name = "Entity1",
                    Properties = new[]
                    {
                        new EntityProperty { Name = "Prop1", DataType = DataType.Boolean  },
                        new EntityProperty { Name = "Prop2", DataType = DataType.String  },
                        new EntityProperty { Name = "Prop3", DataType = DataType.Integer  },
                        new EntityProperty { Name = "Prop4", DataType = DataType.Real  },
                    }
                },
                new Entity
                {
                    Name = "Entity2"
                }
            }
        };

        [Test]
        public void ScribanGeneratorCSharpTest()
        {
            var generator = new ScribanGenerator();

            var result = generator.Generate(model);
            Console.WriteLine(result);
        }

        [Test]
        public void T4GeneratorCSharpTest()
        {
            var generator = new T4Generator();

            var result = generator.Generate(model);
            Console.WriteLine(result);
        }

        [Test]
        public void RazorGeneratorCSharpTest()
        {
            var generator = new RazorGenerator();

            var result = generator.Generate(model);
            Console.WriteLine(result);
        }
    }
}