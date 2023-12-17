/*********************************************************
 * Copyright (c) 2019-2024 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// String扩展
    /// </summary>
    public static partial class _String
    {
        /// <summary>
        /// 校验是否为空
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }

        /// <summary>
        /// 正则匹配
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static bool IsMatch(this string @this, string regex)
        {
            try
            {
                return Regex.IsMatch(@this, regex);
            }
            catch
            {
                return false;
            }
        }
    }
}
