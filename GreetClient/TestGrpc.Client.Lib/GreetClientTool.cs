using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TestGrpc;
using static TestGrpc.Client.Lib.Greeter;

namespace TestGrpc.Client.Lib
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
            var credentials = new SslCredentials(GetCertPem());
            channel = new Channel("localhost", 5012, credentials);
            client = new GreeterClient(channel);
            reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
        }
    }

    
}
