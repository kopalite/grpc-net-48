This repo is a proof-of-concept for the  gRPC service implemented in .NET 5+ and a client in .NET Classic 4.7.2.
It leans on the GreeterService MSDN example, with a twist of having a client implemented in older framework,
which is suitable for legacy projects that still need to call the gRPC endpoints.
The solution contains Server and Client folders, with separate solutions.

Setup: 

Before you proceed with testing, we need to deal with certificates.
Being said, you will need a local development certificate, for server authentication and transport encryption.
Generally, certificates can be found in two, so called, certificate stores: CurrentUser (run certmgr.msc) and LocalMachine (run certmgr).
The server and client in this solution allows both stores, by alternating StoreLocation property in config files.
The most common development certificate used is with subject localhost. In order to create it, use the one of two options
(don't forget to alter StoreLocation config property, otherwise the server and client might not be able to find it).

	1. in CurrentUser store, by dotnet dev-certs
	   create-cert-cu.cmd (source: https://www.hanselman.com/blog/developing-locally-with-aspnet-core-under-https-ssl-and-selfsigned-certs)
	   set server's app.config and key "StoreLocation" to CurrentUser. Do the same with client's appsettings.json and GreetServerConfig.StoreLocation property.

	2. in LocalMachine store, by PowerShell
	   create-cert-lm.ps1 (source: https://devblogs.microsoft.com/aspnet/configuring-https-in-asp-net-core-across-different-platforms/)
	   set server's app.config and key "StoreLocation" to LocalMachine. Do the same with client's appsettings.json and GreetServerConfig.StoreLocation property.
       if using this option, you need run certmgr and copy localhost to Trusted Root Certification Authorities

After this, you need to reference TestGrpc.Client.Nuget inside TestGrpc.Client project,
either through direct project reference (easy way) or through nuget package (hard way).
In order to do it through nuget package, there are some tools prepared for you.
please look at the ~GreetClient/nuget/nuget_pack.cmd file, which will create package locally,
basing od adjacent TestGrpc.Clent.Nuget.nuspec file. After that, you may directly install the package
in TestGrpc.Client, by running this command in Package Manager Console:

install-package "<absolute_repo_path>\GreetClient\nuget\TesetGrpc.Client.Nuget.1.0.0.nupkg"

Following action should be running server and verifying it works, then uncommenting code in TestGrpc.Client - Program.cs and running it:

```
    class Program
    {
        static async Task Main(string[] args)
        {
            //TODO: after making and installing TestGrpc.Client.Nuget.*.nupkg package, uncomment to test the gRPC client.
            //var client = new NuGet.GreetClientTool();
            //await client.GreetPlainAsync();
            //await client.GreetSecureAsync();
            await Task.CompletedTask;
        }   
    }
```