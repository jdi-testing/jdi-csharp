using System;
using System.Reflection;

namespace Epam.JDI.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ComplexAttribute : Attribute
    {
        public static bool IsPresent(FieldInfo field)
        {
            var attribute = field.GetCustomAttribute<ComplexAttribute>(false);
            return attribute != null;
        }
        public static bool IsPresent(PropertyInfo prop)
        {
            var attribute = prop.GetCustomAttribute<ComplexAttribute>(false);
            return attribute != null;
        }
    }
}
