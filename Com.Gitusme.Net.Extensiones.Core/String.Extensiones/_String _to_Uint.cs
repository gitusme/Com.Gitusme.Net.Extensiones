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
        /// ��stringת��Ϊuint16
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static ushort? ToUInt16(this string @this)
        {
            try
            {
                return Convert.ToUInt16(@this);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��stringת��Ϊuint16
        /// </summary>
        /// <param name="this"></param>
        /// <param name="fromBase"></param>
        /// <returns></returns>
        public static ushort? ToUInt16(this string @this, int fromBase)
        {
            try
            {
                return Convert.ToUInt16(@this, fromBase);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��stringת��Ϊuint16
        /// </summary>
        /// <param name="this"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static ushort? ToUInt16(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToUInt16(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��stringת��Ϊuint32
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static uint? ToUInt32(this string @this)
        {
            try
            {
                return Convert.ToUInt32(@this);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��stringת��Ϊuint32
        /// </summary>
        /// <param name="this"></param>
        /// <param name="fromBase"></param>
        /// <returns></returns>
        public static uint? ToUInt32(this string @this, int fromBase)
        {
            try
            {
                return Convert.ToUInt32(@this, fromBase);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��stringת��Ϊuint32
        /// </summary>
        /// <param name="this"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static uint? ToUInt32(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToUInt32(@this, provider);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��stringת��Ϊuint64
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static ulong? ToUInt64(this string @this)
        {
            try
            {
                return Convert.ToUInt64(@this);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��stringת��Ϊuint64
        /// </summary>
        /// <param name="this"></param>
        /// <param name="fromBase"></param>
        /// <returns></returns>
        public static ulong? ToUInt64(this string @this, int fromBase)
        {
            try
            {
                return Convert.ToUInt64(@this, fromBase);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// ��stringת��Ϊuint64
        /// </summary>
        /// <param name="this"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static ulong? ToUInt64(this string @this, IFormatProvider provider)
        {
            try
            {
                return Convert.ToUInt64(@this, provider);
            }
            catch
            {
                return null;
            }
        }
    }
}
