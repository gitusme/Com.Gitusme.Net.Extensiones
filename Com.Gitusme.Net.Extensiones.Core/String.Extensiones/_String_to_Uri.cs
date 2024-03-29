/*********************************************************
 * Copyright (c) 2019-2024 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// String扩展
    /// </summary>
    public static partial class _String
    {
        /// <summary>
        /// 将string转换为uri
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Uri ToUri(this string @this)
        {
            return new Uri(@this);
        }

        /// <summary>
        /// 将string转换为uri
        /// </summary>
        /// <param name="this"></param>
        /// <param name="uriKind"></param>
        /// <returns></returns>
        public static Uri ToUri(this string @this, UriKind uriKind)
        {
            return new Uri(@this, uriKind);
        }
    }
}
