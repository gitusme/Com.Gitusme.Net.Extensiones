/*********************************************************
 * Copyright (c) 2023-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// Socket监听器
    /// </summary>
    public abstract class SocketServerListener : ISocketListener
    {
        /// <summary>
        /// 已启动
        /// </summary>
        public virtual void OnStarted() { }

        /// <summary>
        /// 已停止
        /// </summary>
        public virtual void OnStopped() { }

        /// <summary>
        /// 接收到客户端连接
        /// </summary>
        /// <param name="commandFilter"></param>
        /// <param name="acceptHandler"></param>
        public virtual void OnAccepted(
            CommandFilter commandFilter, ISocketHandler acceptHandler) { }

        /// <summary>
        /// 出错
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnError(Exception e) { }

        /// <summary>
        /// 接收时触发
        /// </summary>
        public virtual void OnReceived() { }

        /// <summary>
        /// 发送时触发
        /// </summary>
        public virtual void OnSend() { }
    }
}
