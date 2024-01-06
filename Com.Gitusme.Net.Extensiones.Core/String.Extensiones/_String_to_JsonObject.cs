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
    /// String��չ
    /// </summary>
    public static partial class _String
    {
        /// <summary>
        /// ��stringת��ΪJsonObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T ToJsonObject<T>(this string @this) where T : class
        {
            return ToJsonObject<T>(@this, default(T));
        }

        /// <summary>
        /// ��stringת��ΪJsonObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="onError"></param>
        /// <returns></returns>
        public static T ToJsonObject<T>(this string @this, Action<Exception> onError) where T : class
        {
            return ToJsonObject<T>(@this, new JsonSerializerOptions(), onError);
        }

        /// <summary>
        /// ��stringת��ΪJsonObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="options"></param>
        /// <param name="onError"></param>
        /// <returns></returns>
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

        /// <summary>
        /// ��stringת��ΪJsonObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public static T ToJsonObject<T>(this string @this, T @default) where T : class
        {
            return ToJsonObject<T>(@this, new JsonSerializerOptions(), @default);
        }

        /// <summary>
        /// ��stringת��ΪJsonObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="options"></param>
        /// <param name="default"></param>
        /// <returns></returns>
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
