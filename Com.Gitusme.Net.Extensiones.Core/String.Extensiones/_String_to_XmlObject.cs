/*********************************************************
 * Copyright (c) 2019-2024 gitusme, All rights reserved.
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
    public static partial class _String
    {
        /// <summary>
        /// 将string转换为XmlObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T ToXmlObject<T>(this string @this) where T : class
        {
            return ToXmlObject<T>(@this, default(T));
        }

        /// <summary>
        /// 将string转换为XmlObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public static T ToXmlObject<T>(this string @this, T @default) where T : class
        {
            T result = ToXmlObject<T>(@this, (Action<Exception>)null);
            return result != null ? result : @default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="onError"></param>
        /// <returns></returns>
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

    }
}
