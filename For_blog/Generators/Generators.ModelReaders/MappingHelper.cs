using AutoMapper;
using Generators.ModelReaders.ReadingModel;
using Microsoft.CodeAnalysis;
using System;
using System.Numerics;

namespace Generators.ModelReaders
{
    internal static class MappingHelper
    {
        private static MapperConfiguration mapperConfiguration;

        static MappingHelper()
        {
            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Entity, Common.EntitiesModel.Entity>();
                cfg.CreateMap<EntityProperty, Common.EntitiesModel.EntityProperty>();
                cfg.CreateMap<DataType, Common.EntitiesModel.DataType>();
            });
        }

        internal static Common.EntitiesModel.Entity[] ToEntitiesModel(this Entity[] model)
        {
            var mapper = mapperConfiguration.CreateMapper();
            return mapper.Map<Common.EntitiesModel.Entity[]>(model);
        }

        internal static Common.EntitiesModel.DataType ToModelDataType(this Type type)
        {
            if (type == typeof(string) || type == typeof(char[]))
                return Common.EntitiesModel.DataType.String;

            if (type == typeof(bool))
                return Common.EntitiesModel.DataType.Boolean;

            if (type == typeof(Int16) || type == typeof(Int32) || type == typeof(Int64)
                || type == typeof(UInt16) || type == typeof(UInt32) || type == typeof(UInt64)
                || type == typeof(BigInteger) || type == typeof(Byte) || type == typeof(SByte))
                return Common.EntitiesModel.DataType.Integer;

            if (type == typeof(float) || type == typeof(double) || type == typeof(decimal))
                return Common.EntitiesModel.DataType.Real;

            throw new ArgumentOutOfRangeException("type");
        }

        internal static Common.EntitiesModel.DataType ToModelDataType(
            this ITypeSymbol type)
        {
            switch (type.SpecialType)
            {
                case SpecialType.System_String:
                    return Common.EntitiesModel.DataType.String;
                case SpecialType.System_Boolean:
                    return Common.EntitiesModel.DataType.Boolean;
                case SpecialType.System_Int16:
                case SpecialType.System_Int32:
                case SpecialType.System_Int64:
                case SpecialType.System_UInt16:
                case SpecialType.System_UInt32:
                case SpecialType.System_UInt64:
                case SpecialType.System_Byte:
                case SpecialType.System_SByte:
                    return Common.EntitiesModel.DataType.Integer;
                case SpecialType.System_Double:
                case SpecialType.System_Single:
                    return Common.EntitiesModel.DataType.Real;

            }

            throw new ArgumentOutOfRangeException("type");
        }
    }
}
