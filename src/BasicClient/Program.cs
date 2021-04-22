using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Grpc.Net.Client;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace GRPCClient
{
    class Program
    {


        static async Task Main(string[] args)
        {
            



            // using (var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole()))
            // {

            ServicePointManager.ServerCertificateValidationCallback += ValidateServerCertificate;

            var loggerFactory = LoggerFactory.Create(logging =>
                {
                    //logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Debug);
                });

            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var httpClient = new HttpClient(httpClientHandler);
            var channel = GrpcChannel.ForAddress("https://contoso.com:3001",
            new GrpcChannelOptions { HttpClient = httpClient });



            // Doesn't work with self signed cert
        //    var channel = GrpcChannel.ForAddress("https://localhost:3001",
        //new GrpcChannelOptions { LoggerFactory = loggerFactory });




                var client = new Greeter.GreeterClient(channel);

                var reply = await client.SayHelloAsync(new HelloRequest { Name = "grpcClient" });

                Console.WriteLine("Greeting: " + reply.Message);

                Console.ReadKey();
            }

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        //}
    }
}
