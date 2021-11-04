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
            using (var loggerFactory = LoggerFactory.Create(logging =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Debug);
                }))
            {

//                var httpClientHandler = new HttpClientHandler();

                //httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
//                httpClientHandler.ServerCertificateCustomValidationCallback = ValidateServerCertificate;
                var httpClient = new HttpClient();
//                var httpClient = new HttpClient(httpClientHandler);

		// 100.65.50.124
		//100.115.64.28
                //var channel = GrpcChannel.ForAddress("http://feather.dev.clarkezone.dev:5000", new GrpcChannelOptions { HttpClient = httpClient, LoggerFactory = loggerFactory });
                var channel = GrpcChannel.ForAddress("https://feather-staging.dev.clarkezone.dev:5000", new GrpcChannelOptions { HttpClient = httpClient, LoggerFactory = loggerFactory });

                var client = new Greeter.GreeterClient(channel);

                var reply = await client.SayHelloAsync(new HelloRequest { Name = "grpcClient" });

                Console.WriteLine("Greeting: " + reply.Message);

                Console.ReadKey();
            }
        }

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
