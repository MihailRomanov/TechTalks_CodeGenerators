using Generators.Common;
using Generators.Common.EntitiesModel;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Linq;

namespace Generators.Implementation.ASTBased
{
    public class DACGenerator : IGenerator<Model>
    {
        public string Generate(Model model)
        {
            var script = GetTablesScript(model.Entities, model.NamespaceName);

            var generator = new Sql150ScriptGenerator();
            generator.GenerateScript(script, out var result);

            return result;
        }

        private TSqlFragment GetTablesScript(Entity[] entities, string namespaceName)
        {
            var batch = new TSqlBatch();
            foreach (var entity in entities)
            {
                batch.Statements.Add(GetTableScript(entity, namespaceName));
            }

            return batch;
        }

        private TSqlStatement GetTableScript(Entity entity, string namespaceName)
        {
            var tableName = new SchemaObjectName();
            tableName.Identifiers.Add(new Identifier
            {
                Value = namespaceName,
                QuoteType = QuoteType.SquareBracket
            });
            tableName.Identifiers.Add(new Identifier
            {
                Value = entity.Name,
                QuoteType = QuoteType.SquareBracket
            });

            var definition = new TableDefinition();
            foreach (var prop in entity.Properties ?? Enumerable.Empty<EntityProperty>())
            {
                definition.ColumnDefinitions.Add(GetColumn(prop));
            }

            return new CreateTableStatement()
            {
                SchemaObjectName = tableName,
                Definition = definition
            };
        }

        private ColumnDefinition GetColumn(EntityProperty prop)
        {
            var nullableConstraint = new NullableConstraintDefinition { Nullable = false };

            var columnDefinition = new ColumnDefinition()
            {
                ColumnIdentifier = new Identifier() { Value = prop.Name },
                DataType = prop.DataType.ToSqlType(),
            };
            columnDefinition.Constraints.Add(nullableConstraint);

            return columnDefinition;
        }
    }
}
