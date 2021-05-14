using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace TestGrpc.Server
{
    public class CertificateReader
    {
        public static X509Certificate2 GetCertificate()
        {    
            var certName = "localhost";
            using var store = new X509Store(StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            var matches = store.Certificates.Find(X509FindType.FindBySubjectName, certName, true);
            if (matches.Count != 1)
            {
                throw new Exception($"Could not conclusively find the certificate {certName}");
            }
            return matches[0];
        }
    }
}
