/*********************************************************
 * Copyright (c) 2023-2024 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// 套接字配置
    /// </summary>
    public class SocketSettings
    {
        private static readonly Encoding ENCODING = Encoding.UTF8;
        private static readonly int BUFFER_SIZE = 2 * 1024 * 1024;

        public Encoding Encoding { get; set; } = ENCODING;

        public int BufferSize { get; set; } = BUFFER_SIZE;

        /// <summary>
        /// 默认配置
        /// </summary>
        public static SocketSettings Default
        {
            get { return new SocketSettings(); }
        }
    }
}
