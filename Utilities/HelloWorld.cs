using CleanCoders.WebServer;
using System;
using System.Net.Sockets;
using System.Text;

public class HelloWorld : ISocketService
{
    public static void MainX(string[] args)
    {
        SocketServer server = new SocketServer(8080, new HelloWorld());
        server.Start();
        Console.WriteLine("Server started.  Press any key to exit");
        Console.ReadKey();

        server.Stop();

    }

    public void Serve(Socket socket)
    {
        string response = "HTTP/1.1 200 OK\n" +
          "Content-Length: 21\n" +
          "\n" +
          "<h1>Hello, world</h1>";

        socket.Send(Encoding.UTF8.GetBytes(response));
        
    }
}
