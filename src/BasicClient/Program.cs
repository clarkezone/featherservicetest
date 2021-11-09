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
				Console.WriteLine("Select environment by pressing key: D Dev S: Staging P: Production L: Localhost");

				var key = Console.ReadLine();

				var httpClient = new HttpClient();

				string targetAddress = "";

				switch (key)
				{
					case "d":
						targetAddress = "http://rapi-c2-n1:5000";
						break;
					case "s":
						targetAddress = "https://feather-staging.dev.clarkezone.dev:5001";
						break;
					case "p":
						targetAddress = "https://feather.dev.clarkezone.dev:5001";
						break;
					case "l":
						targetAddress = "http://localhost:5000";
						break;
				}
				var channel = GrpcChannel.ForAddress(targetAddress, new GrpcChannelOptions { HttpClient = httpClient, LoggerFactory = loggerFactory });

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
