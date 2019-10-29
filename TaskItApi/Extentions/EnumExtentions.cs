using System;
using System.Linq;
using TaskItApi.Attributes;

namespace TaskItApi.Extentions
{
    public static class EnumExtentions
    {
        public static StringValueAttribute GetStringValueAttribute(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            return (StringValueAttribute) type.GetField(name).GetCustomAttributes(typeof(StringValueAttribute), false).SingleOrDefault();
        }
    }
}
