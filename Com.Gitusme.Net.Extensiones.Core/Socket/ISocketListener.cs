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
    public interface ISocketListener
    {
        /// <summary>
        /// 出错
        /// </summary>
        /// <param name="e"></param>
        public void OnError(Exception e);

        /// <summary>
        /// 接收时触发
        /// </summary>
        public void OnReceived();

        /// <summary>
        /// 发送时触发
        /// </summary>
        public void OnSend();
    }
}
