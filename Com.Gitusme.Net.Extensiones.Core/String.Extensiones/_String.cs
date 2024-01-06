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
    /// String��չ
    /// </summary>
    public static partial class _String
    {
        /// <summary>
        /// У���Ƿ�Ϊ��
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }

        /// <summary>
        /// ����ƥ��
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
