/*********************************************************
 * Copyright (c) 2023-2024 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// 命令执行结果解析器
    /// </summary>
    public interface IResultParser
    {
        ICommandResult Parse(byte[] result);
    }
}
