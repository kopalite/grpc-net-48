This repo is a proof-of-concept for the  gRPC service implemented in .NET 5+ and a client in .NET Classic 4.7.2.
It leans on the GreeterService MSDN example, with a twist of having a client implemented in older framework,
which is suitable for legacy projects that still need to call the gRPC endpoints.
The solution contains Server and Client folders, with separate solutions.

Server:

Server side has separate solution/project - GreetServer/TestGrpc.Server.sln.
The solution is buildable, but you can run it only after setup is done 
(the package and certificates need to be setup, see the paragraph below).

Client:

Client side has separate solution and 2 projects - GreetClient/TestGrpc.Client.csproj and GreetClient/TestGrpc.Client.Nuget.csproj.
TestGrpc.Client.csproj is executable calling the server described above. It depends on GreetClient/TestGrpc.Client.Nuget.csproj.
As for proof-of-concept sake, we wanted to test the client being a .NET 4.7.2 nuget package, installed and referenced in another .NET 4.7.2 project (described console app).

Setup: 

Before you proceed with testing, we need to deal with certificates and nuget package creation.
Being said, you will need a local development certificate, for server authentication and transport encryption.
Generally, certificates can be found in two, so called, certificate stores: 
**CurrentUser** (run certmgr.msc to see it) and **LocalMachine** (run certmgr to see it, no .msc extension).
The server and client in this solution allows both stores, by alternating StoreLocation property in config files.
The most common development certificate used is with subject localhost. In order to create it, use the one of two options:


	1. for the CurrentUser cert. store, run the dotnet CertTools\dev-certs.cmd and check the localhost existance by running **certmgr.msc**, then open Personal location.
	   Created certificate is automatically copied from Personal folder to the **Trusted Rooth Authorization Authorities** folder.
	   (source: https://www.hanselman.com/blog/developing-locally-with-aspnet-core-under-https-ssl-and-selfsigned-certs)
	   If using this option, do the following:
	   Change server configuration key - **TestGrpc.Server\appsettings.json** - **GreetServerConfig.StoreLocation** should be **CurrentUser** value. 
	   Change server configuration key - **TestGrpc.Client\app.config** and **<add key ="StoreLocation" value="..."/>** should be **CurrentUser** value.

	2. for the LocalMachine cert. store, run the CertTools\create-cert-lm.ps1 and check the localhost existance by running **certmgr**, then open Personal location.
	   Also, you need to copy and paste certificate from Personal folder to the **Trusted Rooth Authorization Authorities** folder manually.
	   (source: https://devblogs.microsoft.com/aspnet/configuring-https-in-asp-net-core-across-different-platforms/)
	   If using this option, do the following:
	   Change server configuration key - **TestGrpc.Server\appsettings.json** - **GreetServerConfig.StoreLocation** should be **LocalMachine** value. 
	   Change server configuration key - **TestGrpc.Client\app.config** and **<add key ="StoreLocation" value="..."/>** should be **LocalMachine** value.

After this, you need to reference **TestGrpc.Client.Nuget** inside **TestGrpc.Client** project,
either through direct project reference (easy way) or through nuget package (hard way).
In order to do it through nuget package, there are some tools prepared for you.
please look at the **GreetClient\nuget\nuget_pack.cmd** file, which will create package locally,
basing od adjacent TestGrpc.Clent.Nuget.nuspec file. After that, you may directly install the package
in TestGrpc.Client, by running this command in **Package Manager Console**:

install-package "<absolute_repo_path>\GreetClient\nuget\TesetGrpc.Client.Nuget.1.0.0.nupkg"

Running:

1. Run the server solution. It will show up, if working correctly, with the following Console output:

```
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:5011
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: https://localhost:5012
```


2. Uncomment the 3 lines in TestGrpc.Client - Program.cs and run the client itself:

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

It should show the window with the following console output:

```
Greeting: Hello GreeterClient insecure request
Greeting: Hello GreeterClient secure request

```

Server should also show the successfull calls:

```
      Content root path: C:\Work\Samples\grpc-net-48\GreetServer\TestGrpc.Server
info: Microsoft.AspNetCore.Hosting.Diagnostics[1]
      Request starting HTTP/2 POST http://localhost:5011/greet.Greeter/SayHello application/grpc -
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[0]
      Executing endpoint 'gRPC - /greet.Greeter/SayHello'
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[1]
      Executed endpoint 'gRPC - /greet.Greeter/SayHello'
info: Microsoft.AspNetCore.Hosting.Diagnostics[2]
      Request finished HTTP/2 POST http://localhost:5011/greet.Greeter/SayHello application/grpc - - 200 - application/grpc 361.1561ms
info: Microsoft.AspNetCore.Hosting.Diagnostics[1]
      Request starting HTTP/2 POST https://localhost:5012/greet.Greeter/SayHello application/grpc -
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[0]
      Executing endpoint 'gRPC - /greet.Greeter/SayHello'
info: Microsoft.AspNetCore.Routing.EndpointMiddleware[1]
      Executed endpoint 'gRPC - /greet.Greeter/SayHello'
info: Microsoft.AspNetCore.Hosting.Diagnostics[2]
      Request finished HTTP/2 POST https://localhost:5012/greet.Greeter/SayHello application/grpc - - 200 - application/grpc 9.4629ms
```

Problems and lessons learned:

required nuget packages are Grpc, Grpc.Tools, Grpc.Net.Client and Google.Protobuf,
but sometimes, installation of Grpc or Grpc.Tools hangs.

  - solution: not conclusive solution, but helps most of the times:
	  a) Clean the folder ~\AppData\Local\NuGet
	  b) in VS, remove all NuGet package sources, except default one https://api.nuget.org/v3/index.json
	  c) try to install concerning package
	Issue might be caused by VS not being able to reach one of other sources,
	which explains why e.g. on private, non-office workstations, installation works just fine.

.proto files are not markable in Properties window for protobuf compilation correctly, 
so gRPC stub code is not generated. 

   - solution: protoc.exe compiler comes out with Grpc.Tools nuget. Installing nuget and restarting VS usually helps

.net standard 2.0 helps with .proto files being compiled, but combined with 4.7.2 assemblies and gRPC,
 introduces a lot of issues with unresolved collisions between referenced package/dll-s versions.
 
   - solution: use only .NET 4.7.2 assemblies
   
error: Google.Protobuf.Tools proto compilation is only supported by default in a C# project (extension .csproj) 

   - uninstal/install google.protobuf nuget and error should be gone
