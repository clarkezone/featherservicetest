using System.Threading.Tasks;
using GreeterLogic;
using Grpc.Core;
using GRPC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

//https://grafana.com/blog/2021/02/11/instrumenting-a-.net-web-api-using-opentelemetry-tempo-and-grafana-cloud/
//Result: doesn't output traces
builder.Services.AddOpenTelemetryTracing((builder)=> {
    builder.SetResourceBuilder(
        ResourceBuilder.CreateDefault().AddService("feather-http-basic-service"))
            .AddAspNetCoreInstrumentation((options)=>{options.EnableGrpcAspNetCoreSupport=true; })
            .AddConsoleExporter();
});

var app = builder.Build();

//app.Listen("http://127.0.0.1:3000");

//Docker requires this:
//app.Listen("https://*:3001");
app.Listen("https://contoso.com:3001");
app.MapGrpcService<GreeterService>();

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
