using System;

namespace EntityModel
{
    public static class EntityPropertyExtension
    {
        public static Type GetDotNetType(this EntityProperty property)
        {
            switch (property.Type)
            {
                case DataTypes.@bool:
                    return typeof (bool);
                case DataTypes.datetime:
                    return typeof(DateTime);
                case DataTypes.@decimal:
                    return typeof(decimal);
                case DataTypes.@double:
                    return typeof(double);
                case DataTypes.@float:
                    return typeof(float);
                case DataTypes.@int:
                    return typeof(int);
                case DataTypes.@long:
                    return typeof(long);
                case DataTypes.@short:
                    return typeof(short);
                case DataTypes.@string:
                    return typeof(string);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static string GetDotNetTypeString(this EntityProperty property)
        {
            return property.GetDotNetType().FullName;
        }
    }
}
