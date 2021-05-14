dotnet restore ..\TestGrpc.Client.Nuget\TestGrpc.Client.Nuget.csproj
dotnet build ..\TestGrpc.Client.Nuget\TestGrpc.Client.Nuget.csproj --configuration Debug
nuget pack TestGrpc.Client.Nuget.nuspec