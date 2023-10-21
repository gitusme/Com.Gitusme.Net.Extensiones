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
    public static partial class _String
    {
        /// <summary>
        /// 将string转换为bool
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool? ToBool(this string @this)
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

        /// <summary>
        /// 将string转换为bool
        /// </summary>
        /// <param name="this"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static bool? ToBool(this string @this, IFormatProvider provider)
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
    }
}
