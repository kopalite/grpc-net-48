using Grpc.Core;
using System;
using System.Threading.Tasks;
using TestGrpc.Client.NuGet.Certificates;
using TestGrpc.Client.NuGet.Configuration;
using static TestGrpc.Client.NuGet.Greeter;

namespace TestGrpc.Client.NuGet
{
    public class GreetClientTool
    {
        private static readonly GreetClientConfig Config = new GreetClientConfig();

        public async Task GreetPlainAsync()
        {   
            var channel = new Channel(Config.Address, Config.InsecurePort, ChannelCredentials.Insecure);
            var client = new GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient insecure request" });
            Console.WriteLine("Greeting: " + reply.Message);
        }

        public async Task GreetSecureAsync()
        {
            var certPem = new CertificateReader().GetCertificatePem(Config.StoreLocation.GetValueOrDefault(), Config.CertSubjectName);
            var credentials = new SslCredentials(certPem);
            var channel = new Channel(Config.Address, Config.SecurePort, credentials);
            var client = new GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient secure request" });
            Console.WriteLine("Greeting: " + reply.Message);
        }
    }
}
