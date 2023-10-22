/*********************************************************
 * Copyright (c) 2023-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core.Logging
{
    /// <summary>
    /// 日志配置
    /// </summary>
    public class LogSettings
    {
        /// <summary>
        /// 日志级别，默认INFO
        /// </summary>
        public Logger.Level Level { get; set; } = Logger.Level.INFO;

        /// <summary>
        /// 日志输出路径，默认程序集所在路径
        /// </summary>
        public string OutputDir { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "Log");

        /// <summary>
        /// 日志文件大小，默认10M
        /// </summary>
        public long FileSize { get; set; } = 10 * 1024 * 1024;

        /// <summary>
        /// 日志文件个数，默认10个
        /// </summary>
        public int FileCount { get; set; } = 10;

        /// <summary>
        /// 日志文件名
        /// </summary>
        public string FileName { get; set; } = "log.txt";
    }
}
