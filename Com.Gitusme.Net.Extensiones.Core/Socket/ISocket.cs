/*********************************************************
 * Copyright (c) 2023-2024 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// 套接字句柄
    /// </summary>
    public interface ISocketHandler
    {
        /// <summary>
        /// 命令过滤器
        /// </summary>
        CommandFilter CommandFilter { get; }

        /// <summary>
        /// 打开套接字
        /// </summary>
        /// <returns></returns>
        ISocketHandler Open();

        /// <summary>
        /// 关闭套接字
        /// </summary>
        void Close();

        /// <summary>
        /// 套接字句柄编号
        /// </summary>
        int Handle { get; }

        /// <summary>
        /// 是否已连接
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        ICommandResult Send(ICommand command);

        /// <summary>
        /// 接收命令结果
        /// </summary>
        /// <param name="commandFilter"></param>
        /// <returns></returns>
        ICommand Receive(CommandFilter commandFilter);

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        int Send(byte[] buffer);
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="socketFlags"></param>
        /// <returns></returns>
        int Send(byte[] buffer, SocketFlags socketFlags);
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="size"></param>
        /// <param name="socketFlags"></param>
        /// <returns></returns>
        int Send(byte[] buffer, int size, SocketFlags socketFlags);
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="socketFlags"></param>
        /// <returns></returns>
        int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags);

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        int Receive(byte[] buffer);
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="socketFlags"></param>
        /// <returns></returns>
        int Receive(byte[] buffer, SocketFlags socketFlags);
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="size"></param>
        /// <param name="socketFlags"></param>
        /// <returns></returns>
        int Receive(byte[] buffer, int size, SocketFlags socketFlags);
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="socketFlags"></param>
        /// <returns></returns>
        int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags);
    }

    /// <summary>
    /// 服务端套接字句柄
    /// </summary>
    public interface ISocketServerHandler : ISocketHandler
    {
        /// <summary>
        /// 启动监听
        /// </summary>
        /// <returns></returns>
        ISocketServerHandler StartListening();
    }

    /// <summary>
    /// 抽象套接字实现类
    /// </summary>
    public abstract class SocketHandler : ISocketHandler, IDisposable
    {
        internal protected byte[] _buffer = new byte[1024];

        internal protected bool _isClosed = false;

        internal protected ISocketListener _socketListener;
        internal protected Socket _socket;
        internal protected EndPoint _endPoint;
        internal protected CommandFactory _commandFactory;
        internal protected CommandFilter _commandFilter;

        public CommandFilter CommandFilter { get { return this._commandFilter; } }

        public int Handle
        {
            get { return this._socket.Handle.ToInt32(); }
        }

        public bool IsConnected
        {
            get
            {
                if (_socket.Connected)
                {
                    if ((_socket.Poll(0, SelectMode.SelectWrite)) && (!_socket.Poll(0, SelectMode.SelectError)))
                    {
                        try
                        {
                            byte[] buffer = new byte[1];
                            if (_socket.Receive(buffer, SocketFlags.Peek) == 0)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        catch
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        public SocketHandler(
            ISocketListener socketListener, Socket socket, EndPoint endPoint)
        {
            this._socketListener = socketListener;
            this._socket = socket;
            this._endPoint = endPoint;
            this._commandFactory = new DefaultCommandFactory();
            this._commandFilter = new CommandFilter(this._commandFactory);
        }

        public virtual void SetCommandFactory(CommandFactory commandFactory)
        {
            this._commandFactory = commandFactory.OrDefault(new DefaultCommandFactory());
            this._commandFilter = new CommandFilter(this._commandFactory);
        }

        public virtual ISocketHandler Open()
        {
            try
            {
                ISocketHandler handler = OpenSocket();
                this._isClosed = false;
                return handler;
            }
            catch (Exception e)
            {
                this._socketListener?.OnError(e);
                return null;
            }
        }

        protected abstract ISocketHandler OpenSocket();

        public virtual void Close()
        {
            try
            {
                CloseSocket();
                this._isClosed = true;
            }
            catch (Exception e)
            {
                this._socketListener?.OnError(e);
            }
        }

        protected virtual void CloseSocket() { }

        public virtual void Dispose()
        {
            this.Close();
        }

        public ICommandResult Send(ICommand command)
        {
            return HandleSendEvent(() =>
            {
                return InvokeAction(
                    () => { return this._socket.Send(command); });
            });
        }

        public ICommand Receive(CommandFilter commandFilter)
        {
            return HandleSendEvent(() =>
            {
                return InvokeAction(
                    () => { return this._socket.Receive(commandFilter); });
            });
        }

        public int Send(byte[] buffer)
        {
            return HandleSendEvent(() =>
            {
                return InvokeAction(
                    () => { return this._socket.Send(buffer); });
            });
        }

        public int Send(byte[] buffer, SocketFlags socketFlags)
        {
            return HandleSendEvent(() =>
            {
                return InvokeAction(
                    () => { return this._socket.Send(buffer, socketFlags); });
            });
        }

        public int Send(byte[] buffer, int size, SocketFlags socketFlags)
        {
            return HandleSendEvent(() =>
            {
                return InvokeAction(
                    () => { return this._socket.Send(buffer, size, socketFlags); });
            });
        }

        public int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags)
        {
            return HandleSendEvent(() =>
            {
                return InvokeAction(
                    () => { return this._socket.Send(buffer, offset, size, socketFlags); });
            });
        }

        public int Receive(byte[] buffer)
        {
            return HandleReceivedEvent(() =>
            {
                return InvokeAction(
                    () => { return this._socket.Receive(buffer); });
            });
        }

        public int Receive(byte[] buffer, SocketFlags socketFlags)
        {
            return HandleReceivedEvent(() =>
            {
                return InvokeAction(
                    () => { return this._socket.Receive(buffer, socketFlags); });
            });
        }

        public int Receive(byte[] buffer, int size, SocketFlags socketFlags)
        {
            return HandleReceivedEvent(() =>
            {
                return InvokeAction(
                    () => { return this._socket.Receive(buffer, size, socketFlags); });
            });
        }

        public int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags)
        {
            return HandleReceivedEvent(() =>
            {
                return InvokeAction(
                    () => { return this._socket.Receive(buffer, offset, size, socketFlags); });
            });
        }

        protected void InvokeAction(Action action)
        {
            action.Invoke();
        }

        protected T InvokeAction<T>(Func<T> action)
        {
            return action.Invoke();
        }

        private T HandleSendEvent<T>(Func<T> func)
        {
            return HandleEvent(func, true);
        }

        private T HandleReceivedEvent<T>(Func<T> func)
        {
            return HandleEvent(func, false);
        }

        private T HandleEvent<T>(Func<T> func, bool isSend)
        {
            try
            {
                T result = func.Invoke();
                if (isSend)
                {
                    this._socketListener?.OnSend();
                }
                else
                {
                    this._socketListener?.OnReceived();
                }
                return result;
            }
            catch (Exception e)
            {
                this._socketListener?.OnError(e);
                throw e;
            }
        }
    }

    /// <summary>
    /// 客户端套接字实现类
    /// </summary>
    public class ClientSocketHandler : SocketHandler
    {
        public ClientSocketHandler(
            SocketClientListener socketListener, Socket socket, EndPoint endPoint)
            : base(socketListener, socket, endPoint)
        {
        }

        protected override ISocketHandler OpenSocket()
        {
            InvokeAction(() => { this._socket.Connect(this._endPoint); });
            (this._socketListener as SocketClientListener)?.OnConnected();
            return this;
        }

        protected override void CloseSocket()
        {
            InvokeAction(() => {
                this._socket.Shutdown(SocketShutdown.Both);
                this._socket.Disconnect(false);
                this._socket.Close();
            });
            (this._socketListener as SocketClientListener)?.OnDisconnected();
        }
    }

    /// <summary>
    /// 服务端套接字实现类
    /// </summary>
    public class ServerSocketHandler : SocketHandler, ISocketServerHandler
    {
        private int _backlog = 5;
        private List<ISocketHandler> _clients = new List<ISocketHandler>();

        public List<ISocketHandler> AcceptSocketHandlers
        {
            get { return new List<ISocketHandler>(_clients); }
        }

        public ServerSocketHandler(
            SocketServerListener socketListener, Socket socket, EndPoint endPoint, int backlog)
            : base(socketListener, socket, endPoint)
        {
            this._backlog = backlog;
        }

        public ISocketServerHandler StartListening()
        {
            this._socket.Bind(this._endPoint);

            this._socket.Listen(this._backlog);
            (this._socketListener as SocketServerListener)?.OnStarted();

            Thread thread = new Thread((commandFilter) =>
            {
                while (!_isClosed)
                {
                    try
                    {
                        ISocketHandler acceptHandler = AcceptSocket();
                        this._clients.Add(acceptHandler);
                        (this._socketListener as SocketServerListener)?.OnAccepted(
                            (CommandFilter)commandFilter, acceptHandler);
                    }
                    catch (SocketException e) when (e.SocketErrorCode  == SocketError.Interrupted)
                    {
                        // server is closed
                        break;
                    }
                }
            });
            thread.Start(this._commandFilter);

            Thread keepAliveThread = new Thread((clients) =>
            {
                while (!_isClosed)
                {
                    List<ISocketHandler> socketHandlers = new List<ISocketHandler>(
                        clients as List<ISocketHandler>);
                    foreach (ISocketHandler acceptHandler in socketHandlers)
                    {
                        if (!acceptHandler.IsConnected)
                        {
                            this._clients.Remove(acceptHandler);
                            acceptHandler.Close();
                        }
                    }
                    Thread.Sleep(1000);
                }
            });
            keepAliveThread.Start(this._clients);

            return this;
        }

        private ISocketHandler AcceptSocket()
        {
            Socket client = InvokeAction(() => { return this._socket.Accept(); });
            ISocketHandler handler = new AcceptSocketHandler(client);
            return handler;
        }

        protected override ISocketHandler OpenSocket()
        {
            return this;
        }

        protected override void CloseSocket()
        {
            InvokeAction(() => {
                foreach (ISocketHandler acceptHandler in this._clients)
                {
                    acceptHandler.Close();
                }
                _isClosed = true;
                this._socket.Close();
            });
            (this._socketListener as SocketServerListener)?.OnStopped();
        }
    }


    /// <summary>
    /// 服务端套接字实现类
    /// </summary>
    public class AcceptSocketHandler : SocketHandler
    {
        public AcceptSocketHandler(Socket socket)
            : base(null, socket, null)
        {
        }

        protected override ISocketHandler OpenSocket()
        {
            return this;
        }

        protected override void CloseSocket()
        {
            InvokeAction(() => {
                this._socket.Shutdown(SocketShutdown.Both);
                this._socket.Disconnect(false);
                this._socket.Close();
            });
        }
    }
}
