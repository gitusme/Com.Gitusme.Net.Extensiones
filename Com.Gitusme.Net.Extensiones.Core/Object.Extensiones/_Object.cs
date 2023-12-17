/*********************************************************
 * Copyright (c) 2019-2024 gitusme, All rights reserved.
 *********************************************************/

using Com.Gitusme.Net.Extensiones.Core.Logging;
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
        /// 如果对象有值，则执行传入Action行为
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="then"></param>
        public static void IfPresent<T>(this T @this, Action<T> then) where T : class
        {
            if (!IsNull(@this))
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
        public static T OrDefault<T>(this T @this, T @default) where T : class
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

        /// <summary>
        /// 记录DEBUG日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="log"></param>
        public static void Logd<T>(this T @this, string log) where T : class
        {
            Log(Logger.Level.DEBUG, @this?.GetType()?.Name, log);
        }

        /// <summary>
        /// 记录DEBUG日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="tag"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Logd<T>(this T @this, string tag, string format, params string[] args) where T : class
        {
            Log(Logger.Level.DEBUG, tag, string.Format(format, args));
        }

        /// <summary>
        /// 记录DEBUG日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="tag"></param>
        /// <param name="log"></param>
        public static void Logd<T>(this T @this, string tag, string log) where T : class
        {
            Log(Logger.Level.DEBUG, tag, log);
        }

        /// <summary>
        /// 记录INFO日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="log"></param>
        public static void Logi<T>(this T @this, string log) where T : class
        {
            Log(Logger.Level.INFO, @this?.GetType()?.Name, log);
        }

        /// <summary>
        /// 记录INFO日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="tag"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Logi<T>(this T @this, string tag, string format, params string[] args) where T : class
        {
            Log(Logger.Level.INFO, tag, string.Format(format, args));
        }

        /// <summary>
        /// 记录INFO日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="tag"></param>
        /// <param name="log"></param>
        public static void Logi<T>(this T @this, string tag, string log) where T : class
        {
            Log(Logger.Level.INFO, tag, log);
        }

        /// <summary>
        /// 记录WARN日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="log"></param>
        public static void Logw<T>(this T @this, string log) where T : class
        {
            Log(Logger.Level.WARN, @this?.GetType()?.Name, log);
        }

        /// <summary>
        /// 记录WARN日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="tag"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Logw<T>(this T @this, string tag, string format, params string[] args) where T : class
        {
            Log(Logger.Level.WARN, tag, string.Format(format, args));
        }

        /// <summary>
        /// 记录WARN日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="tag"></param>
        /// <param name="log"></param>
        public static void Logw<T>(this T @this, string tag, string log) where T : class
        {
            Log(Logger.Level.WARN, tag, log);
        }

        /// <summary>
        /// 记录ERROR日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="e"></param>
        public static void Loge<T>(this T @this, Exception e) where T : class
        {
            Log(Logger.Level.ERROR, @this?.GetType()?.Name, e.ToString());
        }

        /// <summary>
        /// 记录ERROR日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="log"></param>
        public static void Loge<T>(this T @this, string log) where T : class
        {
            Log(Logger.Level.ERROR, @this?.GetType()?.Name, log);
        }


        /// <summary>
        /// 记录ERROR日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="tag"></param>
        /// <param name="e"></param>
        public static void Loge<T>(this T @this, string tag, Exception e) where T : class
        {
            Log(Logger.Level.ERROR, tag, e.ToString());
        }

        /// <summary>
        /// 记录ERROR日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="tag"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Loge<T>(this T @this, string tag, string format, params string[] args) where T : class
        {
            Log(Logger.Level.ERROR, tag, string.Format(format, args));
        }

        /// <summary>
        /// 记录ERROR日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="tag"></param>
        /// <param name="log"></param>
        public static void Loge<T>(this T @this, string tag, string log) where T : class
        {
            Log(Logger.Level.ERROR, tag, log);
        }

        private static void Log(Logger.Level level, string tag, string format, params string[] args)
        {
            Log(level, tag, string.Format(format, args));
        }

        private static void Log(Logger.Level level, string tag, string log)
        {
            Logger.Instance.Log(level, tag, log);
        }
    }
}
