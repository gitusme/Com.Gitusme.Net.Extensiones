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
        /// ��stringת��Ϊbyte
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte? ToByte(this string @this)
        {
            try
            {
                return Convert.ToByte(@this);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��stringת��Ϊbyte
        /// </summary>
        /// <param name="this"></param>
        /// <param name="fromBase"></param>
        /// <returns></returns>
        public static byte? ToByte(this string @this, int fromBase)
        {
            try
            {
                return Convert.ToByte(@this, fromBase);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��stringת��Ϊbyte
        /// </summary>
        /// <param name="this"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static byte? ToByte(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToByte(@this, provider);
            }
            catch
            {
                return null;
            }
        }
    }
}
