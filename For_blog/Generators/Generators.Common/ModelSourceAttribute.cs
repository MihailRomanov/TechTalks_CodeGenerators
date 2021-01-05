using System;

namespace Generators.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ModelSourceAttribute: Attribute
    {
    }
}
