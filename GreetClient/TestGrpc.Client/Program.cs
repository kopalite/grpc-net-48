using System.Threading.Tasks;

namespace TestGrpc.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //TODO: after installing TestGrpc.Client.Nuget.*.nupkg package, uncomment to test the gRPC client.
            //var client = new NuGet.GreetClientTool();
            //await client.GreetPlainAsync();
            //await client.GreetSecureAsync();
            await Task.CompletedTask;
        }

        
    }
}
