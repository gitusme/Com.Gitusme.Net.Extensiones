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
    /// String��չ
    /// </summary>
    public static partial class _String
    {
        /// <summary>
        /// ��stringת��Ϊbyte[]
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToCharArray(this string @this)
        {
            return ToCharArray(@this, new byte[0]);
        }

        /// <summary>
        /// ��stringת��Ϊbyte[]
        /// </summary>
        /// <param name="this"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public static byte[] ToCharArray(this string @this, byte[] @default)
        {
            try
            {
                return Convert.FromBase64String(@this);
            }
            catch
            {
                return @default;
            }
        }

    }
}
