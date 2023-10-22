/*********************************************************
 * Copyright (c) 2019-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// String��չ
    /// </summary>
    public static partial class _String
    {
        /// <summary>
        /// ��stringת��Ϊuri
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Uri ToUri(this string @this)
        {
            return new Uri(@this);
        }

        /// <summary>
        /// ��stringת��Ϊuri
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
