# featherservicetest
A playground for building minimal grpc service endpoints in donetcore using [featherhttp](https://github.com/featherhttp/framework).

[![.NET](https://github.com/clarkezone/featherservicetest/actions/workflows/dotnet.yml/badge.svg)](https://github.com/clarkezone/featherservicetest/actions/workflows/dotnet.yml)
[![Coverage Status](https://coveralls.io/repos/github/clarkezone/featherservicetest/badge.svg)](https://coveralls.io/github/clarkezone/featherservicetest)

# Backlog
- [x] Get basic working gRPC endpoint running using featherhttp
- [x] Simple unit test
- [X] Dockerize
- [X] Set up basic CI/CD pipeline using github actions
- [X] Enforce PR workflow gated by tests
- [X] Add git versioning https://github.com/dotnet/Nerdbank.GitVersioning
- [X] Dotnetcore cli client
- [X] Bicep for creating ACR infra
- [ ] BLOCKED: Local docker build using selfsigned cert with test client
- [ ] Release branch push prod container to ACR
- [ ] Add editor config
- [ ] Replace greeter with bi-directional streaming gRPC
- [ ] Add logging / tracing
- [ ] Add basic health probe
- [ ] Add basic Prometheus metrics
- [ ] Add Open Telemetry Jaeger distributed tracing
- [ ] Add production SSL cert with keyvault
- [ ] Script to deploy to k3s cluster
- [ ] Script to deploy to ACI
- [ ] Add Project Tye
- [ ] Add OSM support
- [ ] Add DAPR support
- [ ] Golang client
- [ ] Investigate adding code coverage metrics

# Dev setup

## Pre-requs
- VSCode
- Docker
- dotnet 5 RTM

## Steps
1. Build docker image: 
    - `cd src\BasicService`
    - `docker build -t feathertestservice:0 .`
2. Create self-signed cert (Taken from https://docs.microsoft.com/en-us/dotnet/core/additional-tools/self-signed-certificates-guide)
    - `powershell .\localdev\createsscert.ps1`
    - Start `certmgr`
    - Confirm that contoso.com is listed in Trusted Root Certificate Authorities\Certificates
3. Add an entry for contoso.com in hosts file
    - `code C:\Windows\System32\drivers\etc\hosts`
    - add hosts entry `127.0.0.1 contoso.com`
4. Run docker image:
    - `docker run --rm -it -p 3000:3000 -p 3001:3001 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=3001 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_Kestrel__Certificates__Default__Password="password" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/contoso.com.pfx  -e Logging__LogLevel__Microsoft=Debug -e Logging__LogLevel__Grpc=Debug -v /c/certs:/https/ feathertestservice:0`
5. Run client
    - `cd src\BasicClient`
    - `dotnet run`
