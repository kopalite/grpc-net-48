This repo is a proof-of-concept of gRPC service implemented in .NET 5+ and client in .NET Classic 4.7.2.
It leans on the GreeterService MSDN example, with a twist of having client implemented in older framework,
which is suitable for legacy projects that still need to call the gRPC endpoints.
The solution contains Server and Client folders, with separate solutions.
Before you proceed with testing, we need to deal with certificates.

Being said, you will need a local development certificate, for server authentication and transport encryption.
Generally, certificates can be found in two, so called, certificate stores: CurrentUser (run certmgr.msc) and LocalMachine (run certmgr).
The server and client in this solution allows both stores, by alternating StoreLocation property in config files.
The most common development certificate used is with subject localhost. In order to create it, use the one of two options
(don't forget to alter StoreLocation config property, otherwise the server and client might not be able to find it).

	1. in CurrentUser store, by dotnet dev-certs 
	   create-cert-cu.cmd (source https://www.hanselman.com/blog/developing-locally-with-aspnet-core-under-https-ssl-and-selfsigned-certs)

	2. in LocalMachine store, by PowerShell
	   create-cert-lm.ps1 (source: https://devblogs.microsoft.com/aspnet/configuring-https-in-asp-net-core-across-different-platforms/)