using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Com.Gitusme.Net.Extensiones.Core;

namespace Com.Gitusme.Net.Extensiones.Demos
{
    internal class SocketDemo : IExtensionesDemo
    {
        public override void Execute()
        {
            base.Execute();

            Thread serverThread = new Thread(() =>
            {
                CommandFactory factory = new DefaultCommandFactory();
                factory.AddCommand(new ACK());
                factory.AddCommand(new EOM());

                ISocketServerHandler server = SocketBuilder.Builder()
                    .CommandFactory(factory)
                    .AddListener(new ServerSocketListener())
                    .CreateServer("127.0.0.1", 8080, 5)
                    .StartListening();
            });
            serverThread.Start();

            Thread clientThread = new Thread((clientId) =>
            {
                ISocketHandler client = SocketBuilder.Builder()
                    .AddListener(new ClientSocketListener())
                    .CreateClient("127.0.0.1", 8080)
                    .Start();

                string tag = $"{clientId}]";

                CommandExecutor executor = new CommandExecutor(client);

                ICommandResult ack = executor.Execute(new ACK());
                string ackResult = SocketSettings.Default.Encoding.GetString(ack.Get());

                Console.WriteLine($"[{tag}] Command Result: {ackResult}");

                ICommandResult eom = executor.Execute(new EOM());
                string eomResult = SocketSettings.Default.Encoding.GetString(eom.Get());

                Console.WriteLine($"[{tag}] Command Result: {eomResult}");

            });
            clientThread.Start($"CLIENT");
        }

        private byte[] Receive(ISocketHandler handler)
        {
            string data = String.Empty;
            byte[] bytes = new byte[1024];
            while (true)
            {
                int bytesRec = handler.Receive(bytes, SocketFlags.None);
                if (bytesRec > 0 && bytesRec < bytes.Length)
                {
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    break;
                }
            }
            byte[] msg = Encoding.ASCII.GetBytes(data);
            return msg;
        }

    }

    class ClientSocketListener : Core.SocketClientListener
    {
        public override void OnConnected()
        {
            System.Console.WriteLine("Client: OnConnected");
        }

        public override void OnDisconnected()
        {
            System.Console.WriteLine("Client: OnDisconnected");
        }

        public override void OnError(Exception e)
        {
            System.Console.WriteLine("Client: OnError");
        }

        public override void OnSend()
        {
            //System.Console.WriteLine("Client: OnSend");
        }

        public override void OnReceived()
        {
            //System.Console.WriteLine("Client: OnReceived");
        }
    }

    class ServerSocketListener : Core.SocketServerListener
    {
        public override void OnStarted()
        {
            System.Console.WriteLine("Server: OnStarted");
        }

        public override void OnStopped()
        {
            System.Console.WriteLine("Server: OnStopped");
        }

        public override void OnAccepted(
            CommandFilter commandFilter, ISocketHandler acceptHandler)
        {
            System.Console.WriteLine("Server: OnAccepted");
            Thread aceeptThread = new Thread((accept) =>
            {
                var acceptHandler = accept as ISocketHandler;

                while (true)
                {
                    try
                    {
                        CommandReceiver receiver = new CommandReceiver(acceptHandler);
                        ICommand command = receiver.Receive(commandFilter);
                        Console.WriteLine("Server received: {0}", command.GetCommand());

                        string msg = $"{command.GetCommand()}, SUCCESS";
                        ICommandResult result = command.GetResultParser().Parse(
                            SocketSettings.Default.Encoding.GetBytes(msg));
                        acceptHandler.Send(result.Get());
                    }
                    catch (NotSupportedException e)
                    {
                        string msg = e.Message;
                        acceptHandler.Send(SocketSettings.Default.Encoding.GetBytes(msg));
                    }

                    Thread.Sleep(1000);
                }

                //while (true)
                //{
                //    var acceptHandler = accept as ISocketHandler;

                //    byte[] receiveBytes = Receive(acceptHandler);
                //    Console.WriteLine("Server received: {0}",
                //        Encoding.ASCII.GetString(receiveBytes));
                //    byte[] sendBytes = Encoding.ASCII.GetBytes("I am gitusme.");
                //    acceptHandler?.Send(sendBytes);
                //}
            });
            aceeptThread.Start(acceptHandler);
        }

        //private byte[] Receive(ISocketHandler handler)
        //{
        //    string data = String.Empty;
        //    byte[] bytes = new byte[1024];
        //    while (true)
        //    {
        //        int bytesRec = handler.Receive(bytes, SocketFlags.None);
        //        if (bytesRec > 0 && bytesRec < bytes.Length)
        //        {
        //            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
        //            break;
        //        }
        //    }
        //    byte[] msg = Encoding.ASCII.GetBytes(data);
        //    return msg;
        //}

        public override void OnError(Exception e)
        {
            System.Console.WriteLine("Server: OnError");
        }

        public override void OnSend()
        {
            //System.Console.WriteLine("Server: OnSend");
        }

        public override void OnReceived()
        {
            //System.Console.WriteLine("Server: OnReceived");
        }
    }
}
