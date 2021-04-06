This project was created with:


`dotnet new feather -n BasicService`
`dotnet add .\BasicService.csproj package Grpc.AspNetCore`
`code greater.proto`

```proto
syntax = "proto3";

option csharp_namespace = "GRPC";

package greet;

// The greeting service definition.
service Greeter {
  // Sends a greeting
  rpc SayHello (HelloRequest) returns (HelloReply);
}

// The request message containing the user's name.
message HelloRequest {
  string name = 1;
}

// The response message containing the greetings.
message HelloReply {
  string message = 1;
}
```

`dotnet grpc add-file .\greater.proto`

`code program.cs`

```cs
using System.Threading.Tasks;
using Grpc.Core;
using GRPC;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.Listen("https://localhost:3000");

app.MapGrpcService<GreeterService>();

await app.RunAsync();

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
```