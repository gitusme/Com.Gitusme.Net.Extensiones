/*********************************************************
 * Copyright (c) 2019-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// String扩展
    /// </summary>
    public static class _String
    {
        //
        // 摘要:
        //     Converts the specified string, which encodes binary data as base-64 digits, to
        //     an equivalent 8-bit unsigned integer array.
        //
        // 参数:
        //   s:
        //     The string to convert.
        //
        // 返回结果:
        //     An array of 8-bit unsigned integers that is equivalent to s.
        public static byte[] ToCharArray(this string @this)
        {
            return ToCharArray(@this, new byte[0]);
        }

        public static byte[] ToCharArray(this string @this, byte[] @default)
        {
            try
            {
                return Convert.FromBase64String(@this);
            }
            catch
            {
                return @default;
            }
        }

        //
        // 摘要:
        //     Converts the specified string representation of a logical value to its Boolean
        //     equivalent.
        //
        // 参数:
        //   value:
        //     A string that contains the value of either System.Boolean.TrueString or System.Boolean.FalseString.
        //
        // 返回结果:
        //     true if value equals System.Boolean.TrueString, or false if value equals System.Boolean.FalseString
        //     or null.
        public static bool? ToBoolean(this string @this)
        {
            try
            {
                return Convert.ToBoolean(@this);
            }
            catch
            {
                return null;
            }
        }

        public static bool? ToBoolean(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToBoolean(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        //
        // 摘要:
        //     Converts the string representation of a number in a specified base to an equivalent
        //     8-bit unsigned integer.
        //
        // 参数:
        //   value:
        //     A string that contains the number to convert.
        //
        //   fromBase:
        //     The base of the number in value, which must be 2, 8, 10, or 16.
        //
        // 返回结果:
        //     An 8-bit unsigned integer that is equivalent to the number in value, or 0 (zero)
        //     if value is null.
        public static byte? ToByte(this string @this)
        {
            try
            {
                return Convert.ToByte(@this);
            }
            catch
            {
                return null;
            }
        }

        public static byte? ToByte(this string @this, int fromBase)
        {
            try
            {
                return Convert.ToByte(@this, fromBase);
            }
            catch
            {
                return null;
            }
        }

        public static byte? ToByte(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToByte(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        //
        // 摘要:
        //     Converts the first character of a specified string to a Unicode character, using
        //     specified culture-specific formatting information.
        //
        // 参数:
        //   value:
        //     A string of length 1 or null.
        //
        //   provider:
        //     An object that supplies culture-specific formatting information. This parameter
        //     is ignored.
        //
        // 返回结果:
        //     A Unicode character that is equivalent to the first and only character in value.
        public static char? ToChar(this string @this)
        {
            try
            {
                return Convert.ToChar(@this);
            }
            catch
            {
                return null;
            }
        }

        public static char? ToChar(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToChar(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        //
        // 摘要:
        //     Converts the specified string representation of a date and time to an equivalent
        //     date and time value.
        //
        // 参数:
        //   value:
        //     The string representation of a date and time.
        //
        // 返回结果:
        //     The date and time equivalent of the value of value, or the date and time equivalent
        //     of System.DateTime.MinValue if value is null.
        public static DateTime? ToDateTime(this string @this)
        {
            try
            {
                return Convert.ToDateTime(@this);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime ToDateTime(this string @this, DateTime @default)
        {
            try
            {
                return Convert.ToDateTime(@this);
            }
            catch
            {
                return @default;
            }
        }

        public static DateTime? ToDateTime(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToDateTime(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        public static DateTime ToDateTime(this string @this, IFormatProvider provider, DateTime @default)
        {
            try
            {
                return Convert.ToDateTime(@this, provider);
            }
            catch
            {
                return @default;
            }
        }

        //
        // 摘要:
        //     Converts the specified string representation of a number to an equivalent decimal
        //     number, using the specified culture-specific formatting information.
        //
        // 参数:
        //   value:
        //     A string that contains a number to convert.
        //
        //   provider:
        //     An object that supplies culture-specific formatting information.
        //
        // 返回结果:
        //     A decimal number that is equivalent to the number in value, or 0 (zero) if value
        //     is null.
        public static decimal? ToDecimal(this string @this)
        {
            try
            {
                return Convert.ToDecimal(@this);
            }
            catch
            {
                return null;
            }
        }

        public static decimal? ToDecimal(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToDecimal(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        //
        // 摘要:
        //     Converts the specified string representation of a number to an equivalent double-precision
        //     floating-point number, using the specified culture-specific formatting information.
        //
        // 参数:
        //   value:
        //     A string that contains the number to convert.
        //
        //   provider:
        //     An object that supplies culture-specific formatting information.
        //
        // 返回结果:
        //     A double-precision floating-point number that is equivalent to the number in
        //     value, or 0 (zero) if value is null.
        public static double? ToDouble(this string @this)
        {
            try
            {
                return Convert.ToDouble(@this);
            }
            catch
            {
                return null;
            }
        }

        public static double? ToDouble(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToDouble(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        //
        // 摘要:
        //     Converts the string representation of a number in a specified base to an equivalent
        //     16-bit signed integer.
        //
        // 参数:
        //   value:
        //     A string that contains the number to convert.
        //
        //   fromBase:
        //     The base of the number in value, which must be 2, 8, 10, or 16.
        //
        // 返回结果:
        //     A 16-bit signed integer that is equivalent to the number in value, or 0 (zero)
        //     if value is null.
        public static short? ToInt16(this string @this)
        {
            try
            {
                return Convert.ToInt16(@this);
            }
            catch
            {
                return null;
            }
        }

        public static short? ToInt16(this string @this, int fromBase)
        {
            try
            {
                return Convert.ToInt16(@this, fromBase);
            }
            catch
            {
                return null;
            }
        }

        public static short? ToInt16(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToInt16(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        //
        // 摘要:
        //     Converts the string representation of a number in a specified base to an equivalent
        //     32-bit signed integer.
        //
        // 参数:
        //   value:
        //     A string that contains the number to convert.
        //
        //   fromBase:
        //     The base of the number in value, which must be 2, 8, 10, or 16.
        //
        // 返回结果:
        //     A 32-bit signed integer that is equivalent to the number in value, or 0 (zero)
        //     if value is null.
        public static int? ToInt32(this string @this)
        {
            try
            {
                return Convert.ToInt32(@this);
            }
            catch
            {
                return null;
            }
        }

        public static int? ToInt32(this string @this, int fromBase)
        {
            try
            {
                return Convert.ToInt32(@this, fromBase);
            }
            catch
            {
                return null;
            }
        }

        public static int? ToInt32(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToInt32(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        //
        // 摘要:
        //     Converts the string representation of a number in a specified base to an equivalent
        //     64-bit signed integer.
        //
        // 参数:
        //   value:
        //     A string that contains the number to convert.
        //
        //   fromBase:
        //     The base of the number in value, which must be 2, 8, 10, or 16.
        //
        // 返回结果:
        //     A 64-bit signed integer that is equivalent to the number in value, or 0 (zero)
        //     if value is null.
        public static long? ToInt64(this string @this)
        {
            try
            {
                return Convert.ToInt64(@this);
            }
            catch
            {
                return null;
            }
        }

        public static long? ToInt64(this string @this, int fromBase)
        {
            try
            {
                return Convert.ToInt64(@this, fromBase);
            }
            catch
            {
                return null;
            }
        }

        public static long? ToInt64(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToInt64(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        //
        // 摘要:
        //     Converts the string representation of a number in a specified base to an equivalent
        //     8-bit signed integer.
        //
        // 参数:
        //   value:
        //     A string that contains the number to convert.
        //
        //   fromBase:
        //     The base of the number in value, which must be 2, 8, 10, or 16.
        //
        // 返回结果:
        //     An 8-bit signed integer that is equivalent to the number in value, or 0 (zero)
        //     if value is null.
        public static sbyte? ToSByte(this string @this)
        {
            try
            {
                return Convert.ToSByte(@this);
            }
            catch
            {
                return null;
            }
        }

        public static sbyte? ToSByte(this string @this, int fromBase)
        {
            try
            {
                return Convert.ToSByte(@this, fromBase);
            }
            catch
            {
                return null;
            }
        }

        public static sbyte? ToSByte(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToSByte(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        //
        // 摘要:
        //     Converts the specified string representation of a number to an equivalent single-precision
        //     floating-point number, using the specified culture-specific formatting information.
        //
        // 参数:
        //   value:
        //     A string that contains the number to convert.
        //
        //   provider:
        //     An object that supplies culture-specific formatting information.
        //
        // 返回结果:
        //     A single-precision floating-point number that is equivalent to the number in
        //     value, or 0 (zero) if value is null.
        public static float? ToSingle(this string @this)
        {
            try
            {
                return Convert.ToSingle(@this);
            }
            catch
            {
                return null;
            }
        }

        public static float? ToSingle(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToSingle(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        //
        // 摘要:
        //     Converts the string representation of a number in a specified base to an equivalent
        //     16-bit unsigned integer.
        //
        // 参数:
        //   value:
        //     A string that contains the number to convert.
        //
        //   fromBase:
        //     The base of the number in value, which must be 2, 8, 10, or 16.
        //
        // 返回结果:
        //     A 16-bit unsigned integer that is equivalent to the number in value, or 0 (zero)
        //     if value is null.
        public static ushort? ToUInt16(this string @this)
        {
            try
            {
                return Convert.ToUInt16(@this);
            }
            catch
            {
                return null;
            }
        }

        public static ushort? ToUInt16(this string @this, int fromBase)
        {
            try
            {
                return Convert.ToUInt16(@this, fromBase);
            }
            catch
            {
                return null;
            }
        }

        public static ushort? ToUInt16(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToUInt16(@this, provider);
            }
            catch
            {
                return null;
            }
        }


        //
        // 摘要:
        //     Converts the string representation of a number in a specified base to an equivalent
        //     32-bit unsigned integer.
        //
        // 参数:
        //   value:
        //     A string that contains the number to convert.
        //
        //   fromBase:
        //     The base of the number in value, which must be 2, 8, 10, or 16.
        //
        // 返回结果:
        //     A 32-bit unsigned integer that is equivalent to the number in value, or 0 (zero)
        //     if value is null.
        public static uint? ToUInt32(this string @this)
        {
            try
            {
                return Convert.ToUInt32(@this);
            }
            catch
            {
                return null;
            }
        }

        public static uint? ToUInt32(this string @this, int fromBase)
        {
            try
            {
                return Convert.ToUInt32(@this, fromBase);
            }
            catch
            {
                return null;
            }
        }

        public static uint? ToUInt32(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToUInt32(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        //
        // 摘要:
        //     Converts the string representation of a number in a specified base to an equivalent
        //     64-bit unsigned integer.
        //
        // 参数:
        //   value:
        //     A string that contains the number to convert.
        //
        //   fromBase:
        //     The base of the number in value, which must be 2, 8, 10, or 16.
        //
        // 返回结果:
        //     A 64-bit unsigned integer that is equivalent to the number in value, or 0 (zero)
        //     if value is null.
        public static ulong? ToUInt64(this string @this)
        {
            try
            {
                return Convert.ToUInt64(@this);
            }
            catch
            {
                return null;
            }
        }

        public static ulong? ToUInt64(this string @this, int fromBase)
        {
            try
            {
                return Convert.ToUInt64(@this, fromBase);
            }
            catch
            {
                return null;
            }
        }

        public static ulong? ToUInt64(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToUInt64(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// To xml Object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T ToXmlObject<T>(this string @this) where T : class
        {
            return ToXmlObject<T>(@this, default(T));
        }

        public static T ToXmlObject<T>(this string @this, T @default) where T : class
        {
            T result = ToXmlObject<T>(@this, (Action<Exception>)null);
            return result != null ? result : @default;
        }

        public static T ToXmlObject<T>(this string @this, Action<Exception> onError) where T : class
        {
            T result = default(T);
            try
            {
                string temp = $@"{AppDomain.CurrentDomain.BaseDirectory}{Guid.NewGuid().ToString()}";
                using (StreamWriter writer = new StreamWriter(temp))
                {
                    writer.Write(@this);
                }
                using (StreamReader reader = new StreamReader(temp))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    result = (T)serializer.Deserialize(reader);
                }
                File.Delete(temp);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
            return result;
        }

        /// <summary>
        /// To json object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T ToJsonObject<T>(this string @this) where T : class
        {
            return ToJsonObject<T>(@this, default(T));
        }

        public static T ToJsonObject<T>(this string @this, Action<Exception> onError) where T : class
        {
            return ToJsonObject<T>(@this, new JsonSerializerOptions(), onError);
        }

        public static T ToJsonObject<T>(this string @this, JsonSerializerOptions options, Action<Exception> onError) where T : class
        {
            T result = default(T);
            try
            {
                result = JsonSerializer.Deserialize<T>(@this, options);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
            return result;
        }

        public static T ToJsonObject<T>(this string @this, T @default) where T : class
        {
            return ToJsonObject<T>(@this, new JsonSerializerOptions(), @default);
        }

        public static T ToJsonObject<T>(this string @this, JsonSerializerOptions options, T @default) where T : class
        {
            T result = @default;
            try
            {
                result = JsonSerializer.Deserialize<T>(@this, options);
            }
            catch { }
            return result;
        }
    }
}
