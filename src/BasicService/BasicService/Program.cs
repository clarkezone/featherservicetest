using System.Threading.Tasks;
using GreeterLogic;
using Grpc.Core;
using GRPC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Loki;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();


builder.Configuration.AddEnvironmentVariables(prefix:"basicservice_");
builder.Configuration.AddCommandLine(args);

builder.WebHost.ConfigureKestrel(
		options => {
		options.ConfigureEndpointDefaults(listen => {
				
			listen.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;	
				});
		}
		);

// TODO replace Serilog with Otel->Loki
// TODO how to change log levels in prod
builder.Host.UseSerilog((context, configuration)
	=>
        {
            var logendpoint = context.Configuration["logendpoint"];
            var credentials = new NoAuthCredentials(logendpoint);
            configuration
                .Enrich
                .FromLogContext()
                .Enrich
                .WithProperty("Application", context.HostingEnvironment.ApplicationName)
                .Enrich
                .WithProperty("Environment", context.HostingEnvironment.EnvironmentName);

            configuration
                .WriteTo
                .Console();

            if(logendpoint != null)
            {
                configuration
                .WriteTo
                .LokiHttp(credentials);
            }
        }
	);

var app = builder.Build();

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
