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
        /// ��stringת��Ϊint16
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
        /// ��stringת��Ϊint16
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
        /// ��stringת��Ϊint16
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
        /// ��stringת��Ϊint32
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
        /// ��stringת��Ϊint32
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
        /// ��stringת��Ϊint32
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
        /// ��stringת��Ϊint64
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
        /// ��stringת��Ϊint64
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
        /// ��stringת��Ϊint64
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
