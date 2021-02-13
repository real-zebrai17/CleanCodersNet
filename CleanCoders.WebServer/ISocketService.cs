using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace CleanCoders.WebServer
{
    public interface ISocketService
    {
        public void Serve(Socket socket);
    }
}
