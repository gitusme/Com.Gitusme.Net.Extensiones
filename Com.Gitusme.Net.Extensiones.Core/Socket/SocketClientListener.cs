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
    public abstract class SocketClientListener : ISocketListener
    {
        /// <summary>
        /// 已连接
        /// </summary>
        public virtual void OnConnected() { }

        /// <summary>
        /// 已断开
        /// </summary>
        public virtual void OnDisconnected() { }

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
