# featherservicetest
A playground for building minimal grpc service endpoints using featherhttp.

# Backlog
- [x] Get basic working gRPC endpoint running
- [x] Simple unit test
- [X] Dockerize
- [X] Set up basic CI/CD pipeline
- [ ] Enforce PR workflow gated by tests
- [ ] Add git versioning https://github.com/dotnet/Nerdbank.GitVersioning
- [ ] Add versioned docker build to CI/CD for main / dev branches
- [ ] Bicep for creating ACR infra
- [ ] Main branch push container to ACR
- [ ] Replace greeter with bi-directional streaming gRPC
- [ ] Dotnetcore cli client
- [ ] Flutter GUI client
- [ ] Add logging
- [ ] Add basic health probe
- [ ] Add basic Prometheus metrics
- [ ] Add Jaeger distributed tracing
- [ ] Script to deploy to k3s cluster
- [ ] Script to deploy to ACI
- [ ] Add DAPR support
- [ ] Investigate adding code coverage metrics

[![.NET](https://github.com/clarkezone/featherservicetest/actions/workflows/dotnet.yml/badge.svg)](https://github.com/clarkezone/featherservicetest/actions/workflows/dotnet.yml)