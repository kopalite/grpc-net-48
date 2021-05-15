using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography.X509Certificates;
using TestGrpc.Server.Configuration;

namespace TestGrpc.Server.Certificates
{
    internal class CertificateReader
    {
        public static X509Certificate2 GetCertificate(StoreLocation storeLocation, string certSubjectName)
        {    
            using var store = new X509Store(storeLocation);
            store.Open(OpenFlags.ReadOnly);
            var matches = store.Certificates.Find(X509FindType.FindBySubjectName, certSubjectName, true);
            if (matches.Count != 1)
            {
                throw new Exception($"Could not conclusively find the certificate {certSubjectName}");
            }
            return matches[0];
        }
    }
}
