/*********************************************************
 * Copyright (c) 2023-2024 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// 命令接收器
    /// </summary>
    public class CommandReceiver
    {
        private ISocketHandler _socketHandler;

        public CommandReceiver(ISocketHandler socketHandler)
        {
            this._socketHandler = socketHandler;
        }

        /// <summary>
        /// 接收命令
        /// </summary>
        /// <param name="commandFilter"></param>
        /// <returns></returns>
        public ICommand Receive(CommandFilter commandFilter)
        {
            return this._socketHandler.Receive(commandFilter);
        }
    }
}
