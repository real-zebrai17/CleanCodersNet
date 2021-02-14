using CleanCoders.Specs.TestDoubles;
using CleanCoders.WebServer;
using CleanCoders.View;

using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using CleanCoders.UseCases;
using CleanCoders.UseCases.CodeCastSummaries;

namespace CleanCoders
{
    public class Main_ish
    {
        class MainSocketService : ISocketService
        {
            public void Serve(Socket socket)
            {
                socket.Send(MakeResponse(GetFrontPage()));
                socket.Close();
            }

            private byte[] MakeResponse(string content)
            {
                return
                    Encoding.UTF8.GetBytes(
                        $"HTTP/1.1 200 OK\n" +
                        $"Content-Length: {content.Length}\n" +
                        $"Access-Control-Allow-Origin: *\n" +
                        $"\n" +
                        $"{content}");
            }

            private string GetFrontPage()
            {
                var useCase = new CodecastSummariesUseCase();
                var presentableCodeCasts = useCase.PresentCodeCasts(Context.UserGateway.FindUserByUserName("Micah"));

                var frontPageTemplate = ViewTemplate.Create("Resources/html/frontpage.html");


                StringBuilder codeCastLines = new StringBuilder();
                foreach(var presentableCodeCast in presentableCodeCasts)
                {
                    var codecastTemplate = ViewTemplate.Create("Resources/html/codecast.html");
                    codecastTemplate.Replace("title", presentableCodeCast.Title);
                    codecastTemplate.Replace("publicationDate", presentableCodeCast.Title);

                    //staged 
                    codecastTemplate.Replace("thumbnail", "data:image/jpeg;base64,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");
                    codecastTemplate.Replace("author", "Uncle Bob");
                    codecastTemplate.Replace("duration", "58 min.");
                    codecastTemplate.Replace("contentActions", "Buying options go here");


                    codeCastLines.Append(codecastTemplate.GetContent());
                }
                
                frontPageTemplate.Replace("CODECASTS", codeCastLines.ToString());

                return frontPageTemplate.GetContent();
            }
        }

        public static void Main(string[] args)
        {
            FixtureSetup.SetupSampleData();
            var server = new SocketServer(8080, new MainSocketService());
            server.Start();
            Console.ReadKey();
        }
    }
}
