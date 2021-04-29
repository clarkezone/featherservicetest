# featherservicetest
A playground for building minimal grpc service endpoints in donetcore using [featherhttp](https://github.com/featherhttp/framework).

[![.NET](https://github.com/clarkezone/featherservicetest/actions/workflows/dotnet.yml/badge.svg)](https://github.com/clarkezone/featherservicetest/actions/workflows/dotnet.yml)
[![Coverage Status](https://coveralls.io/repos/github/clarkezone/featherservicetest/badge.svg)](https://coveralls.io/github/clarkezone/featherservicetest)

# Backlog Phase 1
Theme: Hello world for frontend, Infra
- [x] Get basic working gRPC endpoint running using featherhttp
- [x] Simple unit test
- [X] Dockerize
- [X] Set up basic CI/CD pipeline using github actions
- [X] Enforce PR workflow gated by tests
- [X] Add git versioning https://github.com/dotnet/Nerdbank.GitVersioning
- [X] Dotnetcore cli client
- [X] Bicep for creating ACR infra
- [X] Local docker build works using selfsigned cert with test client
- [X] Release branch push prod container to ACR
- [X] Figure out ACR login via docker
- [X] Fix client-side logging
- [X] Investigate adding code coverage metrics
- [ ] Add editor config
- [ ] Add service-side logging / tracing
- [ ] Add self-signed cert support when hosted on Linux
- [ ] Add basic Prometheus metric publishing
- [ ] Replace greeter with bi-directional simple streaming ping/pong gRPC
- [ ] Helm chart to deploy to K8s
- [ ] Add basic health probe
- [ ] Script / action to deploy to k3s cluster
- [ ] Script / action to deploy to ACI

# Backlog Phase 2
Theme: Hello microservice
- [ ] Add production SSL cert with Azure keyvault
- [ ] Add a backend microsevice (eg a simple in-memory queue)
- [ ] Add Open Telemetry Jaeger distributed tracing
- [ ] Add Project Tye
- [ ] Add OSM support
- [ ] Add DAPR support
- [ ] Golang client
? Authentication for front end gRPC

# Backlog Phase 3
Theme: Moah backend service integration

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
