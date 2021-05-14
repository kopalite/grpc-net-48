dotnet restore ..\TestGrpc.Client.Nuget\TestGrpc.Client.Nuget.csproj
dotnet build ..\TestGrpc.Client.Nuget\TestGrpc.Client.Nuget.csproj --configuration Release
nuget pack TestGrpc.Client.Nuget.nuspec