using Grpc.Core;
using System;
using System.Threading.Tasks;
using static TestGrpc.Client.NuGet.Greeter;

namespace TestGrpc.Client.NuGet
{
    public class GreetClientTool
    {
        private static readonly ConfigReader Config = new ConfigReader();

        public async Task GreetPlainAsync()
        {
            var channel = new Channel(Config.InsecureAddress, Config.InsecurePort, ChannelCredentials.Insecure);
            var client = new GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
        }

        public async Task GreetSecureAsync()
        {
            var certPem = new CertificateReader().GetCertificatePem(Config.CertSubjectName);
            var credentials = new SslCredentials(certPem);
            var channel = new Channel(Config.SecureAddress, Config.SecurePort, credentials);
            var client = new GreeterClient(channel);
            var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
        }
    }
}
