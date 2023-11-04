/*********************************************************
 * Copyright (c) 2023-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Com.Gitusme.Net.Extensiones.Core
{
    public class SocketSettings
    {
        private static readonly Encoding ENCODING = Encoding.UTF8;
        private static readonly int BUFFER_SIZE = 2 * 1024 * 1024;

        public Encoding Encoding { get; set; } = ENCODING;

        public int BufferSize { get; set; } = BUFFER_SIZE;

        public static SocketSettings Default
        {
            get { return new SocketSettings(); }
        }
    }

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
            byte[] bytes = @this.ReadBytes();
            ICommandResult commandResult = command.GetResultParser().Parse(bytes);
            return commandResult;
        }

        public static ICommand Receive(this Socket @this)
        {
            byte[] bytes = ReadBytes(@this);
            string command = SocketSettings.Default.Encoding.GetString(bytes);

            Dictionary<string, ICommand> parameters = new Dictionary<string, ICommand>();
            ICommand ack = new ACK();
            parameters.Add(ack.GetCommand(), ack);
            ICommand eom = new EOM();
            parameters.Add(eom.GetCommand(), eom);

            if (parameters.ContainsKey(command))
            {
                return parameters[command];
            }
            return null;
        }

        private static byte[] ReadBytes(this Socket @this)
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

    /// <summary>
    /// 抽象命令
    /// </summary>
    public abstract class AbstractCommand : ICommand
    {
        public ICommandResult Send(ISocketHandler socketHandler)
        {
            return socketHandler.Send(this);
        }

        public abstract string GetCommand();

        public abstract IResultParser GetResultParser();
    }

    public class ACK : AbstractCommand
    {
        public override string GetCommand()
        {
            return "<|ACK|>";
        }

        public override IResultParser GetResultParser()
        {
            return new StringResultParser();
        }
    }

    public class EOM : AbstractCommand
    {
        public override string GetCommand()
        {
            return "<|EOM|>";
        }

        public override IResultParser GetResultParser()
        {
            return new StringResultParser();
        }
    }

    public class CommandReceiver
    {
        private ISocketHandler _socketHandler;
        public CommandReceiver(ISocketHandler socketHandler)
        {
            this._socketHandler = socketHandler;
        }

        public ICommand Receive()
        {
            return this._socketHandler.Receive();
        }
    }

    public class CommandExecutor
    {
        private ISocketHandler _socketHandler;
        public CommandExecutor(ISocketHandler socketHandler)
        {
            this._socketHandler = socketHandler;
        }

        public ICommandResult Execute(ICommand command)
        {
            return command.Send(this._socketHandler);
        }
    }

    public interface IResultParser
    {
        ICommandResult Parse(byte[] result);
    }

    public abstract class AbstractResultParser : IResultParser
    {
        public abstract ICommandResult Parse(byte[] result);
    }

    public class StringResultParser : AbstractResultParser
    {
        public override ICommandResult Parse(byte[] bytes)
        {
            return new DefaultCommandResult(bytes);
        }
    }

    public interface ICommandResult
    {
        byte[] Get();
    }

    public class DefaultCommandResult : ICommandResult
    {
        private byte[] _bytes;

        public DefaultCommandResult(byte[] bytes)
        {
            this._bytes = bytes;
        }

        public byte[] Get()
        {
            return this._bytes;
        }
    }
}
