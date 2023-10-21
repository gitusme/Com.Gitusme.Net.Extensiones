/*********************************************************
 * Copyright (c) 2019-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// Object扩展
    /// </summary>
    public static partial class _Object
    {
        /// <summary>
        /// 将对象转换为Json文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T @this) where T : class
        {
            return ToJson<T>(@this, new JsonSerializerOptions());
        }

        /// <summary>
        /// 将对象转换为Json文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T @this, Encoding encoding) where T : class
        {
            return ToJson<T>(@this, new JsonSerializerOptions(), encoding);
        }

        /// <summary>
        /// 将对象转换为Json文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="options"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 将对象转换为Json文本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="options"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
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
