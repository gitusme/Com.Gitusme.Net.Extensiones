/*********************************************************
 * Copyright (c) 2023-2024 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Net.Sockets;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// Socket扩展
    /// </summary>
    public static partial class _Socket
    {
        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="this"></param>
        /// <param name="command"></param>
        public static ICommandResult Send(this Socket @this, ICommand command)
        {
            @this.Send(SocketSettings.Default.Encoding.GetBytes(command.GetCommand()));
            byte[] bytes = @this.ReceiveBytes();
            ICommandResult commandResult = command.GetResultParser().Parse(bytes);
            return commandResult;
        }

        /// <summary>
        /// 读取命令结果
        /// </summary>
        /// <param name="this"></param>
        /// <param name="commandFilter"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static ICommand Receive(this Socket @this, CommandFilter commandFilter)
        {
            byte[] bytes = ReceiveBytes(@this);
            string cmd = SocketSettings.Default.Encoding.GetString(bytes);
            ICommand command = commandFilter.Filter(cmd);
            if (command.IsNull())
            {
                throw new NotSupportedException($"Not Supported Command: {cmd}");
            }
            return command;
        }

        private static byte[] ReceiveBytes(this Socket @this)
        {
            string data = String.Empty;
            byte[] bytes = new byte[SocketSettings.Default.BufferSize];
            while (true)
            {
                int bytesRec = @this.Receive(bytes, SocketFlags.None);
                if (bytesRec > 0 && bytesRec <= bytes.Length)
                {
                    data += SocketSettings.Default.Encoding.GetString(bytes, 0, bytesRec);
                    break;
                }
            }
            byte[] msg = SocketSettings.Default.Encoding.GetBytes(data);
            return msg;
        }
    }
}
