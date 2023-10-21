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
        /// 判断对象是否为null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T @this) where T : class
        {
            return @this == null;
        }

        /// <summary>
        /// 判断对象是否不为null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>(this T @this) where T : class
        {
            return !IsNull(@this);
        }

        /// <summary>
        /// 如果对象有值，则执行传入Action行为
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="then"></param>
        public static void IfPresent<T>(this T @this, Action<T> then) where T : class
        {
            if (IsNotNull(@this))
            {
                then?.Invoke(@this);
            }
        }

        /// <summary>
        /// 如果对象为null，则返回传入的默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public static T OrDefault<T>(this T @this, T @default)
        {
            return @this != null ? @this : @default;
        }

        /// <summary>
        /// 如果对象为null，则返回传入的默认值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public static T OrDefault<T>(this T? @this, T @default) where T : struct
        {
            return @this.GetValueOrDefault(@default);
        }
    }
}
