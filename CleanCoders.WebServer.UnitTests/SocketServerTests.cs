using NUnit.Framework;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CleanCoders.WebServer.UnitTests
{
    public class SocketServerTests
    {
        private int _port;
        private FakeSocketService _service;
        private SocketServer _server;

        public class FakeSocketService : ISocketService
        {
            public int Connections { get; private set; }

            public void Serve(Socket socket)
            {
                Connections++;
                socket.Close();
            }
        }
    

        [SetUp]
        public void SetUp()
        {
            _port = 8042;
            _service = new FakeSocketService();
            _server = new SocketServer(_port, _service);
        }

        [TearDown]
        public void TearDown()
        {
            _server.Stop();
        }

        [Test]
        public void Instansiate()
        {
            Assert.AreEqual(_port, _server.Port);
            Assert.AreEqual(_service, _server.Service);
        }

        [Test]
        public void CanStartAndStopServer()
        {
            _server.Start();
            Assert.True(_server.IsRunning);
            _server.Stop();
            Assert.False(_server.IsRunning);
        }

        [Test]
        public void acceptsAnIncommingConntection()
        {
            _server.Start();
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(IPAddress.Parse("127.0.0.1"), _port);
            Thread.Sleep(1); //Hack to setup order of operation

            Assert.AreEqual(1, _service.Connections);
        }
    }
}