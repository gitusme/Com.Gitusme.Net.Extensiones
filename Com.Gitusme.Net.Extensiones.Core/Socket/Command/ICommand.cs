/*********************************************************
 * Copyright (c) 2023-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// 命令接口
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="socketHandler"></param>
        /// <returns></returns>
        ICommandResult Send(ISocketHandler socketHandler);

        /// <summary>
        /// 将命令转为byte[]
        /// </summary>
        /// <returns></returns>
        string GetCommand();

        /// <summary>
        /// 结果解析器
        /// </summary>
        /// <returns></returns>
        IResultParser GetResultParser();
    }
}
