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
        /// ��stringת��Ϊdecimal
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static decimal? ToDecimal(this string @this)
        {
            try
            {
                return Convert.ToDecimal(@this);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��stringת��Ϊdecimal
        /// </summary>
        /// <param name="this"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static decimal? ToDecimal(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToDecimal(@this, provider);
            }
            catch
            {
                return null;
            }
        }
    }
}
