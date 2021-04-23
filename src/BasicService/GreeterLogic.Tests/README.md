For adding Coverlet support:

* `dotnet add .\GreeterLogic.Tests.csproj package coverlet.msbuild`

To run tests with coverage reports

* `dotnet test .\GreeterLogic.Tests\ /p:CollectCoverage=true /p:CoverletOutput=TestResults/ /p:CoverletOutputFormat=lcov`