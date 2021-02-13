using NUnit.Framework;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CleanCoders.WebServer.UnitTests
{
    [TestFixture]
    public class SocketServerTests
    {
        class WithClosingSocketService
        {
            private int _port;
            private ClosingSocketService _service;
            private SocketServer _server;

            public class ClosingSocketService : TestSocketService
            {
                public int Connections { get; private set; }

                protected override void DoService(Socket socket)
                {
                    Connections++;
                }
            }

            [SetUp]
            public void SetUp()
            {
                _port = 8042;
                _service = new ClosingSocketService();
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
                lock (_service)
                {
                    Monitor.Wait(_service);
                }

                _server.Stop();

                Assert.AreEqual(1, _service.Connections);
            }

            [Test]
            public void acceptsMultipleIncommingConntections()
            {
                _server.Start();
                var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                var socket2 = new Socket(SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(IPAddress.Parse("127.0.0.1"), _port);
                lock (_service)
                {
                    Monitor.Wait(_service);
                }

                socket2.Connect(IPAddress.Parse("127.0.0.1"), _port);
                lock (_service)
                {
                    Monitor.Wait(_service);
                }

                _server.Stop();

                Assert.AreEqual(2, _service.Connections);
            }
        }


        public class WithReadingService {

            private int _port;
            private ReadingSocketService _service;
            private SocketServer _server;
            
            public class ReadingSocketService : TestSocketService
            {
                public string Message { get; set; }


                protected override void DoService(Socket socket)
                {
                    var buffer = new byte[100];
                    var length = socket.Receive(buffer);
                    Message = Encoding.UTF8.GetString(buffer).Substring(0, length);
                }
            }

            [SetUp]
            public void SetUp()
            {
                _port = 8042;
                _service = new ReadingSocketService();
                _server = new SocketServer(_port, _service);
            }

            [TearDown]
            public void TearDown()
            {
                _server.Stop();
            }

            [Test]
            public void CanSendAndRecievedData()
            {
                var _server = new SocketServer(_port, _service);
                _server.Start();

                var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(IPAddress.Parse("127.0.0.1"), _port);
                socket.Send(Encoding.UTF8.GetBytes("Hello\n"));

                lock (_service)
                {
                    Monitor.Wait(_service);
                }


                Assert.AreEqual("Hello\n", _service.Message);
                _server.Stop();
            }
        }

        public class WithEchoService
        {

            private int _port;
            private EchoSocketService _service;
            private SocketServer _server;

            public class EchoSocketService : TestSocketService
            {
                public byte[] _buffer;


                protected override void DoService(Socket socket)
                {
                    ReadFrom(socket);
                    WriteTo(socket);
                }

                private void WriteTo(Socket socket)
                {
                    socket.Send(_buffer);
                }

                private void ReadFrom(Socket socket)
                {
                    var buffer = new byte[100];
                    var length = socket.Receive(buffer);
                    
                    _buffer = new byte[length];
                    Array.Copy(buffer, _buffer, length);
                }
            }

            [SetUp]
            public void SetUp()
            {
                _port = 8042;
                _service = new EchoSocketService();
                _server = new SocketServer(_port, _service);
            }

            [TearDown]
            public void TearDown()
            {
                _server.Stop();
            }

            [Test]
            public void CanEcho()
            {
                var _server = new SocketServer(_port, _service);
                _server.Start();

                var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(IPAddress.Parse("127.0.0.1"), _port);
                socket.Send(Encoding.UTF8.GetBytes("Echo Echo\n"));

                lock (_service)
                {
                    Monitor.Wait(_service);
                }

                var buffer = new byte[100];
                var length = socket.Receive(buffer);
                var response = Encoding.UTF8.GetString(buffer).Substring(0, length);

                Assert.AreEqual("Echo Echo\n", response);
                _server.Stop();
            }
        }

        public abstract class TestSocketService : ISocketService
        {
            public void Serve(Socket socket)
            {
                DoService(socket);

                
                lock (this)
                {
                    Monitor.Pulse(this);
                }

                socket.Close();
            }
            protected abstract void DoService(Socket socket);
        }
    }
}