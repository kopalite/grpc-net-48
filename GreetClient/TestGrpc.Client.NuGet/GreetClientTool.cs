using Grpc.Core;
using System;
using System.Threading.Tasks;
using static TestGrpc.Client.NuGet.Greeter;

namespace TestGrpc.Client.NuGet
{
    public class GreetClientTool
    {
        public async Task GreetPlainAsync()
        {
            var channel = new Channel("localhost", 5011, ChannelCredentials.Insecure);
            var client = new GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
        }

        public async Task GreetSecureAsync()
        {
            var certPem = new CertificateReader().GetCertPem();
            var credentials = new SslCredentials(certPem);
            var channel = new Channel("localhost", 5012, credentials);
            var client = new GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
        }
    }
}
