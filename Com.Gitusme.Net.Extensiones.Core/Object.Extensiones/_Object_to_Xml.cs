/*********************************************************
 * Copyright (c) 2019-2024 gitusme, All rights reserved.
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
        /// 将对象序列化为Xml文本。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns>Xml文本</returns>
        public static string ToXml<T>(this T @this) where T : class
        {
            return ToXml<T>(@this, Encoding.Default);
        }

        /// <summary>
        /// 将对象序列化为Xml文本。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="encoding">编码格式</param>
        /// <returns>Xml文本</returns>
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
    }
}
