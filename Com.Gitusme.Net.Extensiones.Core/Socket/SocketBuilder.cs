/*********************************************************
 * Copyright (c) 2023-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Com.Gitusme.Net.Extensiones.Core
{
    /// <summary>
    /// Socket接口
    /// </summary>
    public sealed class SocketBuilder
    {
        private Lazy<Socket> _socket;
        private IPEndPoint _endPoint;
        private ISocketListener _socketListener;
        private CommandFactory _commandFactory;
        private bool _blocking = true;

        private SocketBuilder()
        {
        }

        public static SocketBuilder Builder()
        {
            return new SocketBuilder();
        }

        public SocketBuilder AddListener<T>(T socketListener) where T : ISocketListener
        {
            this._socketListener = socketListener;
            return this;
        }

        public SocketBuilder CommandFactory<T>(T commandFactory) where T : CommandFactory
        {
            this._commandFactory = commandFactory;
            return this;
        }

        public SocketBuilder Blocking(bool blocking)
        {
            this._blocking = blocking;
            return this;
        }

        public ISocketHandler CreateClient(string host, int port)
        {
            Create(host, port);

            Socket socket = this._socket.Value;
            socket.Blocking = this._blocking;
            ClientSocketHandler handler = new ClientSocketHandler(
                (this._socketListener as SocketClientListener), socket, this._endPoint);
            handler.SetCommandFactory(this._commandFactory);
            return handler;
        }

        public ISocketServerHandler CreateServer(string host, int port, int backlog)
        {
            Create(host, port);

            Socket socket = this._socket.Value;
            socket.Blocking = this._blocking;
            ServerSocketHandler handler = new ServerSocketHandler(
                (this._socketListener as SocketServerListener), socket, this._endPoint, backlog);
            handler.SetCommandFactory(this._commandFactory);
            return handler;
        }

        private SocketBuilder Create(string host, int port)
        {
            return Create(host, port, SocketType.Stream, ProtocolType.Tcp);
        }

        private SocketBuilder Create(IPAddress ipAddress, int port)
        {
            return Create(ipAddress, port, SocketType.Stream, ProtocolType.Tcp);
        }

        private SocketBuilder Create(string host, int port, SocketType socketType, ProtocolType protocolType)
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(host);
            IPAddress ipAddress = hostEntry.AddressList[0];

            return Create(ipAddress, port, socketType, protocolType);
        }

        private SocketBuilder Create(IPAddress ipAddress, int port, SocketType socketType, ProtocolType protocolType)
        {
            this._endPoint = new IPEndPoint(ipAddress, port);
            this._socket = new Lazy<Socket>(() =>
            {
                try
                {
                    return new Socket(ipAddress.AddressFamily, socketType, protocolType);
                }
                catch (SocketException e)
                {
                    this._socketListener?.OnError(e);
                    throw e;
                }
            });

            return this;
        }
    }
}
