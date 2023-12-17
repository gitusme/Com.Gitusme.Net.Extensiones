/*********************************************************
 * Copyright (c) 2023-2024 gitusme, All rights reserved.
 *********************************************************/

using Com.Gitusme.Net.Extensiones.Core.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Com.Gitusme.Net.Extensiones.Core.Logging
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public sealed class Logger
    {
        private static readonly object _lock = new object();

        private static Logger _instance;

        private Level _level;
        private string _outputDir;
        private string _fileName;
        private long _fileSize;
        private int _fileCount;

        private FileInfo _logFile;

        /// <summary>
        /// 获取日志组件实例
        /// </summary>
        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Logger();
                        }
                    }
                }
                return _instance;
            }
        }

        private Logger()
        {
            Init(Settings.Load().LogSettings);
        }


        /// <summary>
        /// 初始化日志组件
        /// </summary>
        /// <param name="settings"></param>
        public void Init(LogSettings settings)
        {
            lock (_lock)
            {
                _level = settings.Level;
                _outputDir = settings.OutputDir;
                _fileName = settings.FileName;
                _fileSize = settings.FileSize;
                _fileCount = settings.FileCount;

                _logFile = GetLogFile();
                _logFile.Directory.Create();
            }
        }

        /// <summary>
        /// 打印日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="tag">标签</param>
        /// <param name="format">格式串</param>
        /// <param name="args">参数</param>
        public void Log(Level level, string tag, string format, params string[] args)
        {
            Log(level, tag, string.Format(format, args));
        }

        /// <summary>
        /// 打印日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="tag">标签</param>
        /// <param name="content">内容</param>
        public void Log(Level level, string tag, string content)
        {
            if (level >= _level)
            {
                Log($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.000")} [{level}] [{Thread.CurrentThread.ManagedThreadId.ToString("D5")}] {tag}: {content}");
            }
        }

        private void Log(string log)
        {
            lock (_lock)
            {
                if (!_logFile.Exists || _logFile.Length >= _fileSize)
                {
                    _logFile = GetLogFile();
                }

                if (!_logFile.Exists)
                {
                    _logFile.Create().Close();
                }

                using (var writer = File.AppendText(_logFile.FullName))
                {
                    writer.WriteLine(log);
                }
            }
        }

        private FileInfo GetLogFile()
        {
            string currFileName;
            string currTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            int index = _fileName.LastIndexOf('.');
            if (index == -1)
            {
                currFileName = $"{_fileName}-{currTime}";
            }
            else
            {
                currFileName = $"{_fileName.Substring(0, index)}-{currTime}.{_fileName.Substring(index + 1, _fileName.Length - index - 1)}";
            }

            string filePath = Path.Combine(_outputDir, currFileName);
            return new FileInfo(filePath);
        }

        /// <summary>
        /// 日志级别
        /// </summary>
        public enum Level
        {
            /// <summary>
            /// 关闭
            /// </summary>
            OFF,
            /// <summary>
            /// 严重
            /// </summary>
            FATAL,
            /// <summary>
            /// 错误
            /// </summary>
            ERROR,
            /// <summary>
            /// 警告
            /// </summary>
            WARN,
            /// <summary>
            /// 信息
            /// </summary>
            INFO,
            /// <summary>
            /// 调试
            /// </summary>
            DEBUG,
            /// <summary>
            /// 追踪
            /// </summary>
            TRACE,
            /// <summary>
            /// 全部
            /// </summary>
            ALL
        }
    }
}
