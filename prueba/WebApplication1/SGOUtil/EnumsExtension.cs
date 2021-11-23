using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGOUtil
{
    public static class EnumsExtension
    {
        public static string GetStringFromEnum<T>(this string valueString) where T : struct, IConvertible
        {
            string response = string.Empty;
            Array values = Enum.GetValues(typeof(T));
            foreach (T val in values)
            {
                var name = Enum.GetName(typeof(T), val);
                if (name.Equals(valueString))
                {
                    response = name;
                    break;
                }
            }
            return response;
        }

        public static int? GetEnumFromString<T>(this string valueString) where T : struct, IConvertible
        {
            int? response = null;
            Array values = Enum.GetValues(typeof(T));
            for (int i = 0; i < values.Length - 1; i++)
            {
                string name = values.GetValue(i).ToString();
                if (name.Equals(valueString))
                {
                    response = i;
                    break;
                }
            }
            return response;
        }

        public static T ToEnumValue<T>(this string str) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val;
            return Enum.TryParse<T>(str, true, out val) ? val : default(T);
        }

        public static T ToEnumValue<T>(this int intValue) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }

            return (T)Enum.ToObject(enumType, intValue);
        }
    }//end class
}
