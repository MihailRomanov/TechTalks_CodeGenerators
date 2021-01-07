using Generators.Common.EntitiesModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System;

namespace Generators.Implementation
{
    public static class ModelExtensions
    {
        public static Type ToNetType(this DataType type)
        {
            switch (type)
            {
                case DataType.Boolean:
                    return typeof(bool);
                case DataType.String:
                    return typeof(string);
                case DataType.Integer:
                    return typeof(int);
                case DataType.Real:
                    return typeof(double);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        public static string ToNetTypeString(this DataType type)
        {
            return type.ToNetType().FullName;
        }

        public static SyntaxNode ToTypeNode(this DataType type, SyntaxGenerator generator)
        {
            switch (type)
            {
                case DataType.Boolean:
                    return generator.TypeExpression(SpecialType.System_Boolean);
                case DataType.String:
                    return generator.TypeExpression(SpecialType.System_String);
                case DataType.Integer:
                    return generator.TypeExpression(SpecialType.System_Int32);
                case DataType.Real:
                    return generator.TypeExpression(SpecialType.System_Double);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        public static SqlDataTypeReference ToSqlType(this DataType type)
        {
            switch (type)
            {
                case DataType.Boolean:
                    return new SqlDataTypeReference
                    {
                        SqlDataTypeOption = SqlDataTypeOption.Bit
                    };
                case DataType.String:
                    return new SqlDataTypeReference
                    {
                        SqlDataTypeOption = SqlDataTypeOption.NVarChar
                    };
                case DataType.Integer:
                    return new SqlDataTypeReference
                    {
                        SqlDataTypeOption = SqlDataTypeOption.Int
                    };
                case DataType.Real:
                    return new SqlDataTypeReference
                    {
                        SqlDataTypeOption = SqlDataTypeOption.Real
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }
    }
}
