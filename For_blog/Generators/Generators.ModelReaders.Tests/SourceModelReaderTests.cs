using FluentAssertions;
using Generators.Common.EntitiesModel;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis.MSBuild;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Generators.ModelReaders.Tests
{
    public class SourceModelReaderTests
    {
        [Test]
        public async Task ReadFromSolutionTest()
        {
            MSBuildLocator.RegisterDefaults();
            var workspace = MSBuildWorkspace.Create();

            var solutionPath = Path.GetFullPath(
                Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\..\..\Generators.sln"));

            var solution = await workspace.OpenSolutionAsync(solutionPath);

            var project = solution.Projects.Where(p => p.Name == "Generators.ModelReaders.Tests").First();
            var compilation = await project.GetCompilationAsync();

            var model = SourceCodeModelReader.Read(compilation);

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