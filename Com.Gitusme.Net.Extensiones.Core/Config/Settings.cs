/*********************************************************
 * Copyright (c) 2023-2024 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace Com.Gitusme.Net.Extensiones.Core.Config
{
    /// <summary>
    /// 组件配置
    /// </summary>
    [XmlRoot("Settings")]
    public class Settings
    {
        private static readonly string _config = Path.Combine(
            Directory.GetCurrentDirectory(), Assembly.GetExecutingAssembly().GetName().Name + ".Settings.xml");

        private static Settings _default = new Settings();

        /// <summary>
        /// 默认设定
        /// </summary>
        public static Settings Default => _default;

        /// <summary>
        /// 加载设定
        /// </summary>
        /// <returns></returns>
        public static Settings Load()
        {
            Settings settings = null;
            if (!File.Exists(_config) || (settings = File.ReadAllText(_config)?.ToXmlObject<Settings>()).IsNull())
            {
                File.WriteAllText(_config, _default.ToXml());
            }
            return settings.OrDefault(Default);
        }

        /// <summary>
        /// 日志设定
        /// </summary>
        [XmlElement("LogSettings")]
        public LogSettings LogSettings { get; set; } = new LogSettings();
    }
}
