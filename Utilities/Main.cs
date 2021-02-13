using CleanCoders.Specs.TestDoubles;
using CleanCoders.WebServer;
using System;
using System.Net.Sockets;
using System.Text;

namespace CleanCoders
{
    public class Main_ish
    {
        class MainSocketService : ISocketService
        {
            public void Serve(Socket socket)
            {
                socket.Send(MakeResponse(GetFrontPage()));
            }

            private byte[] MakeResponse(string content)
            {
                return
                    Encoding.UTF8.GetBytes(
                        $"HTTP/1.1 200 OK\n" +
                        $"Content-Length: {content.Length}\n" +
                        $"\n" +
                        $"{content}");
            }

            private string GetFrontPage()
            {
                return "Gunk!";
            }
        }

        public static void Main(string[] args)
        {
            FixtureSetup.SetupContext();
            var server = new SocketServer(8080, new MainSocketService());
            server.Start();
            Console.ReadKey();
        }
    }
}
