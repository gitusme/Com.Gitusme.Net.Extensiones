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
    public interface ISocketHandler
    {
        CommandFilter CommandFilter { get; }

        ISocketHandler Start();

        void Close();

        ICommandResult Send(ICommand command);

        ICommand Receive(CommandFilter commandFilter);


        int Send(byte[] buffer);
        int Send(byte[] buffer, SocketFlags socketFlags);
        int Send(byte[] buffer, int size, SocketFlags socketFlags);
        int Send(byte[] buffer, int offset, int size, SocketFlags socketFlags);

        int Receive(byte[] buffer);
        int Receive(byte[] buffer, SocketFlags socketFlags);
        int Receive(byte[] buffer, int size, SocketFlags socketFlags);
        int Receive(byte[] buffer, int offset, int size, SocketFlags socketFlags);
    }

    public interface ISocketServerHandler : ISocketHandler
    {
        ISocketServerHandler StartListening();
    }

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

        public virtual ISocketHandler Start()
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

        protected void InvokeAction(Action action, Func<bool> until)
        {
            if (_socket.Blocking)
            {
                action.Invoke();
            }
            else
            {
                while (true)
                {
                    try
                    {
                        if (until != null && until.Invoke())
                        {
                            break;
                        }
                        action.Invoke();
                    }
                    catch (SocketException e) when (e.SocketErrorCode == SocketError.WouldBlock)
                    {
                        continue;
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        protected T InvokeAction<T>(Func<T> action)
        {
            if (_socket.Blocking)
            {
                return action.Invoke();
            }
            else
            {
                while (true)
                {
                    try
                    {
                        return action.Invoke();
                    }
                    catch (SocketException e) when (e.SocketErrorCode == SocketError.WouldBlock)
                    {
                        continue;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                throw new TimeoutException("InvokeAction timeout");
            }
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

    public class ClientSocketHandler : SocketHandler
    {
        public ClientSocketHandler(
            SocketClientListener socketListener, Socket socket, EndPoint endPoint)
            : base(socketListener, socket, endPoint)
        {
        }

        protected override ISocketHandler OpenSocket()
        {
            InvokeAction(
                () => { this._socket.Connect(this._endPoint); },
                () => { return _socket.Connected; });
            (this._socketListener as SocketClientListener)?.OnConnected();
            return this;
        }

        protected virtual void CloseSocket()
        {
            InvokeAction(
                () => { this._socket.Shutdown(SocketShutdown.Both); },
                () => { return true; });
            (this._socketListener as SocketClientListener)?.OnDisconnected();
        }
    }

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
    }

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
                    ISocketHandler acceptHandler = AcceptSocket();
                    this._clients.Add(acceptHandler);
                    (this._socketListener as SocketServerListener)?.OnAccepted(
                        (CommandFilter)commandFilter, acceptHandler);
                }
            });
            thread.Start(this._commandFilter);

            return this;
        }

        private ISocketHandler AcceptSocket()
        {
            Socket client = InvokeAction(
                () => { return this._socket.Accept(); });
            ISocketHandler handler = new AcceptSocketHandler(client);
            return handler;
        }

        protected override ISocketHandler OpenSocket()
        {
            return this;
        }

        protected override void CloseSocket()
        {
            this._socket.Shutdown(SocketShutdown.Both);
            (this._socketListener as SocketServerListener)?.OnStopped();
        }
    }

}
