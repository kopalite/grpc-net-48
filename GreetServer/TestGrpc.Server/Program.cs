using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using TestGrpc.Server.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestGrpc.Server.Certificates;
using System.Net;

namespace TestGrpc.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    webBuilder.ConfigureKestrel(ko =>
                    {
                        ko.ConfigureEndpointDefaults(lo => lo.Protocols = HttpProtocols.Http2);

                        var config = ko.ApplicationServices.GetService<IOptions<GreetServerConfig>>();
                        ko.ListenLocalhost(config.Value.InsecurePort);
                        ko.ListenLocalhost(config.Value.SecurePort, 
                                    lo => lo.UseHttps(CertificateReader.GetCertificate(config.Value.CertSubjectName)));
                    });
                });
    }
}
