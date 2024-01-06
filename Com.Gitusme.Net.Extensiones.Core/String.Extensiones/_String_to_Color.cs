/*********************************************************
 * Copyright (c) 2019-2024 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// String扩展
    /// </summary>
    public static partial class _String
    {
        /// <summary>
        /// 将string转换为Color
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static Color? ToColor(this string @this)
        {
            string htmlColor = @this;

            Color color = Color.Empty;
            // empty color
            if (IsNullOrEmpty(htmlColor))
            {
                return color;
            }

            if (htmlColor[0] == '#')
            {
                // #AARRGGBB
                if (htmlColor.Length == 9)
                {
                    color = Color.FromArgb(
                        Convert.ToInt32(htmlColor.Substring(1, 2), 16),
                        Convert.ToInt32(htmlColor.Substring(3, 2), 16),
                        Convert.ToInt32(htmlColor.Substring(5, 2), 16),
                        Convert.ToInt32(htmlColor.Substring(7, 2), 16));
                    return color;
                }
                // #RRGGBB
                else if (htmlColor.Length == 7)
                {
                    color = Color.FromArgb(
                        Convert.ToInt32(htmlColor.Substring(1, 2), 16),
                        Convert.ToInt32(htmlColor.Substring(3, 2), 16),
                        Convert.ToInt32(htmlColor.Substring(5, 2), 16));
                    return color;
                }
                // #RGB
                else if (htmlColor.Length == 4)
                {
                    string r = Char.ToString(htmlColor[1]);
                    string g = Char.ToString(htmlColor[2]);
                    string b = Char.ToString(htmlColor[3]);

                    color = Color.FromArgb(
                        Convert.ToInt32(r + r, 16),
                        Convert.ToInt32(g + g, 16),
                        Convert.ToInt32(b + b, 16));
                    return color;
                }
                return color;
            }
            else
            {
                return Color.FromName(htmlColor);
            }
        }
    }
}
