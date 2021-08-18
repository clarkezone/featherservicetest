# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

RUN dotnet tool install --tool-path ./ dotnet-certificate-tool

# copy csproj and restore as distinct layers
COPY *.sln .
COPY BasicService/*.csproj ./BasicService/
COPY GreeterLogic/*.csproj ./GreeterLogic/
COPY GreeterLogic.Tests/*.csproj ./GreeterLogic.Tests/
RUN dotnet restore

# copy everything else and build app
COPY BasicService/. ./BasicService/
COPY GreeterLogic/. ./GreeterLogic/
COPY GreeterLogic.Tests/. ./GreeterLogic.Tests/
WORKDIR /source/BasicService
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
#FROM mcr.microsoft.com/dotnet/aspnet:5.0
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim-arm64v8
WORKDIR /app
ENV ASPNETCORE_URLS=https://+;http://+;
EXPOSE 5000
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "BasicService.dll"]
