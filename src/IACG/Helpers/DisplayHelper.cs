using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace IACG.Helpers
{
    public static class DisplayHelper
    {
        public static string GetDisplayNameOfEnumValue<T>(T value) where T : Enum
        {
            var display = GetAttributeOfEnum<T, DisplayAttribute>(value);
            if (display == null)
                return value.ToString();
            else
            {
                return display.Name;
            }
        }

        static T GetAttributeOfEnum<TEnum, T>(TEnum enumVal) where T : System.Attribute where TEnum : Enum
        {
            var memInfo = typeof(TEnum).GetMember(enumVal.ToString());
            return memInfo[0].GetCustomAttribute<T>();
        }
    }
}
