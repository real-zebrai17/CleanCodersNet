using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace CleanCoders.WebServer
{
    public class SocketServer
    {
        private TcpListener _socketServer;
        public ISocketService Service { get; }
        public int Port { get; }
        public bool? IsRunning { get; private  set; }

        public SocketServer(int port, ISocketService service)
        {
            Service = service;
            Port = port;
            _socketServer = new TcpListener(IPAddress.Parse("127.0.0.1"), Port);

        }

        public void Start()
        {
            _socketServer.Start();

            Task.Run(() => Service.Serve(_socketServer.AcceptSocket()));

            IsRunning = true;
        }
        
        public void Stop()
        {
            _socketServer.Stop();
            IsRunning = false;
        }
    }
}
