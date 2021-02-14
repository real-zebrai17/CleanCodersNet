using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace CleanCoders.WebServer
{
    public class SocketServer 
    {
        private TcpListener _socketServer;
        public ISocketService Service { get; }
        public int Port { get; }
        public bool IsRunning { get; private  set; }
        private CancellationTokenSource _tokenSource;

        public SocketServer(int port, ISocketService service)
        {
            Service = service;
            Port = port;
            _socketServer = new TcpListener(IPAddress.Parse("127.0.0.1"), Port);
            _tokenSource =  new CancellationTokenSource();
        }

        public void Start()
        {
            _socketServer.Start();
            IsRunning = true;

            Task.Run(() =>
            {
                while (IsRunning)
                {
                    var socket = _socketServer.AcceptSocket();
                    Task.Run(() => Service.Serve(socket), _tokenSource.Token);
                }
            }, _tokenSource.Token);
        }
        
        public void Stop()
        {
            IsRunning = false;
            _socketServer.Stop();
            _tokenSource.Cancel();
        }
    }
}
