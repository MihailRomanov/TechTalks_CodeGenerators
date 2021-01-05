using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Generators.ModelReaders
{
    public static class SourceCodeModelReader
    {
        public class MarkedTypeVisitor: SymbolVisitor
        {
            private readonly INamedTypeSymbol markAttribute;

            public MarkedTypeVisitor(INamedTypeSymbol markAttribute)
            {
                this.markAttribute = markAttribute;
            }

            public List<INamedTypeSymbol> FoundSymbols { get; } = new List<INamedTypeSymbol>();

            public override void VisitNamespace(INamespaceSymbol symbol)
            {
                foreach (var childSymbol in symbol.GetMembers())
                {
                    childSymbol.Accept(this);
                }
            }

            public override void VisitNamedType(INamedTypeSymbol symbol)
            {
                if (symbol.TypeKind == TypeKind.Class)
                {
                    var isMarked = symbol.GetAttributes()
                        .Where(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, markAttribute))
                        .Any();

                    if (isMarked)
                        FoundSymbols.Add(symbol);
                }
            }
        }

        public static Common.EntitiesModel.Entity[] Read(Compilation compilation)
        {
            var result = new List<Common.EntitiesModel.Entity>();

            var markAttribute =
                compilation.GetTypeByMetadataName("Generators.Common.ModelSourceAttribute");

            var visitor = new MarkedTypeVisitor(markAttribute);
            visitor.Visit(compilation.GlobalNamespace);

            foreach (var type in visitor.FoundSymbols)
            {
                var properties = type.GetMembers()
                    .OfType<IPropertySymbol>()
                    .Select(p => new Common.EntitiesModel.EntityProperty
                    {
                        Name = p.Name,
                        DataType = p.Type.ToModelDataType()
                    });

                var entity = new Common.EntitiesModel.Entity
                {
                    Name = type.Name,
                    Properties = properties.ToArray()
                };

                result.Add(entity);
            }

            return result.ToArray();
        }
    }
}
