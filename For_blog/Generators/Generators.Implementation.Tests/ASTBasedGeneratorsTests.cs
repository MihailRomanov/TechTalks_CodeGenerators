using Fonlow.TypeScriptCodeDom;
using Generators.Common.EntitiesModel;
using Generators.Implementation.ASTBased;
using NUnit.Framework;
using System;

namespace Generators.Implementation.Tests
{
    public class ASTBasedGeneratorsTests
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
        public void CodeDomGeneratorCSharpTest()
        {
            var generator = new CodeDomGenerator();

            var result = generator.Generate(model);
            Console.WriteLine(result);
        }

        [Test]
        public void CodeDomGeneratorVBTest()
        {
            var generator = new CodeDomGenerator("VisualBasic");

            var result = generator.Generate(model);
            Console.WriteLine(result);
        }

        [Test]
        public void CodeDomGeneratorTSTest()
        {
            var generator = new CodeDomGenerator(new TypeScriptCodeProvider(true));

            var result = generator.Generate(model);
            Console.WriteLine(result);
        }

        [Test]
        public void RoslynGeneratorCSharpTest()
        {
            var generator = new RoslynGenerator();

            var result = generator.Generate(model);
            Console.WriteLine(result);
        }

        [Test]
        public void RoslynGeneratorVBTest()
        {
            var generator = new RoslynGenerator("Visual Basic");

            var result = generator.Generate(model);
            Console.WriteLine(result);
        }

        [Test]
        public void DACGeneratorTest()
        {
            var generator = new DACGenerator();

            var result = generator.Generate(model);
            Console.WriteLine(result);
        }
    }
}