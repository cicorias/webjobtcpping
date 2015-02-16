using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTcpServer.Server
{
    public class TcpServer : IDisposable
    {
        int _port;
        public int Port { get { return _port; } }

        bool _running = true;

        public bool Running
        {
            get { return _running; }
            set { _running = value; }
        }

        TcpListener _listener;
        Task _loopTask;

        public TcpServer(int port)
            : this(IPAddress.Any, port)
        {
        }
        public TcpServer(IPAddress listeningAddress, int port)
        {
            _port = port;

            _listener = new TcpListener(listeningAddress, Port);
            _listener.Start();

            _loopTask = Task.Factory.StartNew(ListenLoop);
        }


        private async void ListenLoop()
        {
            while (_running)
            {
                var socket = await _listener.AcceptSocketAsync();
                if (socket == null)
                    break;

                var client = new ClientHandler(this, socket);

                /// Never use this, but suppresses the warning.
                var clientTask = Task.Factory.StartNew(client.HandleRequest);
            }
        }

        public void Dispose()
        {
            if (_loopTask != null)
            {
                _listener.Stop();
                _loopTask.Wait(5000);
                _loopTask.Dispose();
            }
        }

        class ClientHandler
        {
            TcpServer _server;
            Socket _socket;

            public ClientHandler(TcpServer server, Socket socket)
            {
                _server = server;
                _socket = socket;
            }
            public async void HandleRequest()
            {
                while (true)
                {
                    using (var nws = new NetworkStream(_socket, true))
                    using (var sr = new StreamReader(nws))
                    {
                        var line = await sr.ReadLineAsync();
                        if (!_socket.Connected || line == null)
                            return; 
                        
                        var chs = line.ToCharArray();
                        Array.Reverse(chs);
                        var reverseLIne = new string(chs);

                        var responseBytes = System.Text.Encoding.ASCII.GetBytes(reverseLIne);
                        await nws.WriteAsync(responseBytes, 0, responseBytes.Length);
                        await nws.FlushAsync();
                    }
                    return;
                }
            }
        }
    }
}
