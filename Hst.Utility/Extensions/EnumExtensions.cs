using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hst.Utility.Extensions
{
    public static class EnumExtensions
    {
        public static int ToInt(this Enum obj)
        {
            return Convert.ToInt32(obj);
        }
        public static bool IsInEnum<T>(this int obj)
        {
            return Enum.IsDefined(typeof(T), obj);   
        }
        public static bool IsInEnum<T>(this string obj)
        {
            return Enum.IsDefined(typeof(T), obj);
        }
        #region String to Enum
        public static T ParseEnum<T>(string inString, bool ignoreCase = true, bool throwException = true) where T : struct
        {
            return (T)ParseEnum<T>(inString, default(T), ignoreCase, throwException);
        }

        public static T ParseEnum<T>(string inString, T defaultValue,
                               bool ignoreCase = true, bool throwException = false) where T : struct
        {
            T returnEnum = defaultValue;

            if (!typeof(T).IsEnum || String.IsNullOrEmpty(inString))
            {
                throw new InvalidOperationException("Invalid Enum Type or Input String 'inString'. " + typeof(T).ToString() + "  must be an Enum");
            }

            try
            {
                bool success = Enum.TryParse<T>(inString, ignoreCase, out returnEnum);
                if (!success && throwException)
                {
                    throw new InvalidOperationException("Invalid Cast");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Invalid Cast", ex);
            }

            return returnEnum;
        }
        #endregion

        #region Int to Enum
        public static T ParseEnum<T>(int input, bool throwException = true) where T : struct
        {
            return (T)ParseEnum<T>(input, default(T), throwException);
        }
        public static T ParseEnum<T>(int input, T defaultValue, bool throwException = false) where T : struct
        {
            T returnEnum = defaultValue;
            if (!typeof(T).IsEnum)
            {
                throw new InvalidOperationException("Invalid Enum Type. " + typeof(T).ToString() + "  must be an Enum");
            }
            if (Enum.IsDefined(typeof(T), input))
            {
                returnEnum = (T)Enum.ToObject(typeof(T), input);
            }
            else
            {
                if (throwException)
                {
                    throw new InvalidOperationException("Invalid Cast");
                }
            }

            return returnEnum;

        }
        #endregion

        #region String Extension Methods for Enum Parsing
        public static T ToEnum<T>(this string inString, bool ignoreCase = true, bool throwException = true) where T : struct
        {
            return (T)ParseEnum<T>(inString, ignoreCase, throwException);
        }
        public static T ToEnum<T>(this string inString, T defaultValue, bool ignoreCase = true, bool throwException = false) where T : struct
        {
            return (T)ParseEnum<T>(inString, defaultValue, ignoreCase, throwException);
        }
        #endregion

        #region Int Extension Methods for Enum Parsing
        public static T ToEnum<T>(this int input, bool throwException = true) where T : struct
        {
            return (T)ParseEnum<T>(input, default(T), throwException);
        }

        public static T ToEnum<T>(this int input, T defaultValue, bool throwException = false) where T : struct
        {
            return (T)ParseEnum<T>(input, defaultValue, throwException);
        }
        
        #endregion
    }
}
