using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TestGrpc.Client.NuGet
{
    public class CertificateReader
    {
        private string _certificatePem;

        internal string GetCertificatePem(string certificateName)
        {
            if (_certificatePem == null)
            {
                var result = new StringBuilder(Environment.NewLine);
                string certData = null;

                using (var store = new X509Store(StoreLocation.LocalMachine))
                {
                    store.Open(OpenFlags.ReadOnly);

                    var matches = store.Certificates.Find(X509FindType.FindByIssuerName, certificateName, true);
                    if (matches.Count != 1)
                    {
                        throw new Exception($"Could not conclusively find the certificate {certificateName}");
                    }
                    var bytes = matches[0].Export(X509ContentType.Cert);
                    certData = Convert.ToBase64String(bytes);
                }

                do
                {
                    var lineLength = Math.Min(certData.Length, 64);
                    result.AppendLine(certData.Substring(0, lineLength));
                    certData = lineLength < 64 ? null : certData.Substring(64);
                }
                while (certData != null);
                _certificatePem = $"-----BEGIN CERTIFICATE-----{result}-----END CERTIFICATE-----";
            }

            return _certificatePem;
        }
    }
}
