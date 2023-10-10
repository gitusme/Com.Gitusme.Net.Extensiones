/*********************************************************
 * Copyright (c) 2019-2023 gitusme, All rights reserved.
 *********************************************************/

using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Com.Gitusme.Net.Extensiones.Core
{
    public static class _Object
    {
        public static T Or<T>(this T @this, T @default)
        {
            return @this != null ? @this : @default;
        }

        public static T Or<T>(this T? @this, T @default) where T : struct
        {
            return @this.GetValueOrDefault(@default);
        }

        public static string ToXml<T>(this T @this) where T : class
        {
            return ToXml<T>(@this, Encoding.Default);
        }

        public static string ToXml<T>(this T @this, Encoding encoding) where T : class
        {
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(ms, @this);
                    return encoding.GetString(ms.ToArray());
                }
                catch
                {
                    return default(string);
                }
            }
        }

        public static string ToJson<T>(this T @this) where T : class
        {
            return ToJson<T>(@this, new JsonSerializerOptions());
        }

        public static string ToJson<T>(this T @this, Encoding encoding) where T : class
        {
            return ToJson<T>(@this, new JsonSerializerOptions(), encoding);
        }

        public static string ToJson<T>(this T @this, JsonSerializerOptions options) where T : class
        {
            try
            {
                return ToJson<T>(@this, options, Encoding.Default);
            }
            catch
            {
                return default(string);
            }
        }

        public static string ToJson<T>(this T @this, JsonSerializerOptions options, Encoding encoding) where T : class
        {
            try
            {
                byte[] data = Encoding.Default.GetBytes(
                    JsonSerializer.Serialize<T>(@this, options));
                return encoding.GetString(data);
            }
            catch
            {
                return default(string);
            }
        }
    }
}
