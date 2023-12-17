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
        /// 将string转换为sbyte
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 将string转换为sbyte
        /// </summary>
        /// <param name="this"></param>
        /// <param name="fromBase"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 将string转换为sbyte
        /// </summary>
        /// <param name="this"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
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
    }
}
