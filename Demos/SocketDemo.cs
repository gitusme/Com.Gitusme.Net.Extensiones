using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Com.Gitusme.Net.Extensiones.Core;

namespace Com.Gitusme.Net.Extensiones.Demos
{
    namespace Client
    {
        public class ACK : AbstractCommand
        {
            public override string GetCommand()
            {
                return "<|ACK|>";
            }

            public override IResultParser GetResultParser()
            {
                return new ACKResultParser();
            }
        }

        public class ACKResultParser : AbstractResultParser
        {
            public override ACKCommandResult Parse(byte[] result)
            {
                return new ACKCommandResult(result);
            }
        }

        public class ACKCommandResult : ICommandResult
        {
            private byte[] result;

            public ACKCommandResult(byte[] result)
            {
                this.result = result;
            }

            public byte[] Get()
            {
                return this.result;
            }
        }

        public class EOM : AbstractCommand
        {
            public override string GetCommand()
            {
                return "<|EOM|>";
            }
        }
    }

    namespace Server
    {
        public class ACK : AbstractCommand
        {
            public override string GetCommand()
            {
                return "<|ACK|>";
            }

            public override IResultParser GetResultParser()
            {
                return new ACKResultParser();
            }
        }

        public class ACKResultParser : AbstractResultParser
        {
            public override ACKCommandResult Parse(byte[] result)
            {
                return new ACKCommandResult(result);
            }
        }

        public class ACKCommandResult : ICommandResult
        {
            private byte[] result;

            public ACKCommandResult(byte[] result)
            {
                this.result = result;
            }

            public byte[] Get()
            {
                return SocketSettings.Default.Encoding.GetBytes(
                    $"Server said: {SocketSettings.Default.Encoding.GetString(result)}");
            }
        }

        public class EOM : AbstractCommand
        {
            public override string GetCommand()
            {
                return "<|EOM|>";
            }
        }
    }

    internal class SocketDemo : IExtensionesDemo
    {
        public override void Execute()
        {
            base.Execute();

            Thread serverThread = new Thread(() =>
            {
                CommandFactory factory = new DefaultCommandFactory();
                factory.AddUserCommand(new Server.ACK());

                ISocketServerHandler server = SocketBuilder.Builder()
                    .CommandFactory(factory)
                    .AddListener(new ServerSocketListener())
                    .CreateServer("127.0.0.1", 8080, 5)
                    .StartListening();

            });
            serverThread.Start();

            Thread clientThread = new Thread((clientId) =>
            {
                for(int i = 1; i <= 10000; i++)
                {
                    System.Console.WriteLine("==== times=" + i.ToString("D5"));

                    ISocketHandler client = SocketBuilder.Builder()
                        .AddListener(new ClientSocketListener())
                        .CreateClient("127.0.0.1", 8080)
                        .Open();

                    string tag = $"{clientId}";

                    CommandExecutor executor = new CommandExecutor(client);

                    ICommandResult ack = executor.Execute(new Client.ACK());
                    string ackResult = SocketSettings.Default.Encoding.GetString(ack.Get());

                    Console.WriteLine($"[{tag}] ACK Result: {ackResult}");

                    ICommandResult eom = executor.Execute(new Client.EOM());
                    string eomResult = SocketSettings.Default.Encoding.GetString(eom.Get());

                    Console.WriteLine($"[{tag}] EOM Result: {eomResult}");

                    client.Close();

                    Thread.Sleep(200);
                    System.Console.WriteLine();
                }

                System.Console.WriteLine("Exit = 0");
            });
            clientThread.Start($"CLIENT");
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
            System.Console.WriteLine($"Server: OnAccepted Client-{acceptHandler.Handle}");
            Thread aceeptThread = new Thread((accept) =>
            {
                var acceptHandler = accept as ISocketHandler;

                while (acceptHandler.IsConnected)
                {
                    try
                    {
                        CommandReceiver receiver = new CommandReceiver(acceptHandler);
                        ICommand command = receiver.Receive(commandFilter);

                        string msg = $"I received '{command.GetCommand()}'.";
                        ICommandResult result = command.GetResultParser().Parse(
                            SocketSettings.Default.Encoding.GetBytes(msg));
                        acceptHandler.Send(result.Get());
                    }
                    catch (NotSupportedException e)
                    {
                        string msg = e.Message;
                        acceptHandler.Send(SocketSettings.Default.Encoding.GetBytes(msg));
                    }
                    catch (SocketException e) when (e.SocketErrorCode == SocketError.Shutdown
                    || e.SocketErrorCode == SocketError.ConnectionAborted)
                    {
                        break;
                    }
                }
                System.Console.WriteLine($"Client-{acceptHandler.Handle} is exit");
            });
            aceeptThread.Start(acceptHandler);
        }


        public override void OnError(Exception e)
        {
            System.Console.WriteLine("Server: OnError");
        }

    }
}
