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
        /// 将string转换为int16
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 将string转换为int16
        /// </summary>
        /// <param name="this"></param>
        /// <param name="fromBase"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 将string转换为int16
        /// </summary>
        /// <param name="this"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 将string转换为int32
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 将string转换为int32
        /// </summary>
        /// <param name="this"></param>
        /// <param name="fromBase"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 将string转换为int32
        /// </summary>
        /// <param name="this"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 将string转换为int64
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 将string转换为int64
        /// </summary>
        /// <param name="this"></param>
        /// <param name="fromBase"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 将string转换为int64
        /// </summary>
        /// <param name="this"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
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
    }
}
