using System.Threading.Tasks;
using GreeterLogic;
using Grpc.Core;
using GRPC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Loki;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddCommandLine(args);

// TODO replace Serilog with Otel->Loki
// TODO how to change log levels in prod
builder.Host.UseSerilog((context, configuration)
	=>
        {
            var credentials = new NoAuthCredentials("http://localhost:3100"); //TODO replace with configuration
            configuration
                .Enrich
                .FromLogContext()
                .Enrich
                .WithProperty("Application", context.HostingEnvironment.ApplicationName)
                .Enrich
                .WithProperty("Environment", context.HostingEnvironment.EnvironmentName);

            if (context.HostingEnvironment.IsEnvironment("Production"))
            {
                configuration
                .WriteTo
                .LokiHttp(credentials);
            }
            else
            {
                configuration
                .WriteTo
                .LokiHttp(credentials)
                .WriteTo
                .Console();
            }
        }
	);

var app = builder.Build();

//app.Listen("http://127.0.0.1:3000");

//Docker requires this:
//app.Listen("https://*:3001");
app.Listen("https://contoso.com:3001");
app.MapGrpcService<GreeterService>();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

await app.RunAsync();

public class GreeterService : Greeter.GreeterBase
{
	private readonly ILogger<GreeterService> _logger;
	private readonly GreeterProvider _greeter;

	public GreeterService(ILogger<GreeterService> logger)
	{
		_logger = logger;
		_greeter = new GreeterProvider();
	}

	public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
	{
		return Task.FromResult(new HelloReply
		{
			Message = _greeter.SayHello(request.Name)
		}); ; ;
	}
}
