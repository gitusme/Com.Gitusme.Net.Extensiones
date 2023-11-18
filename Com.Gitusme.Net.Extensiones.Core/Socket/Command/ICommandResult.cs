/*********************************************************
 * Copyright (c) 2023-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// 命令执行结果
    /// </summary>
    public interface ICommandResult
    {
        byte[] Get();
    }
}
