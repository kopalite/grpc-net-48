using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace TestGrpc.Client.NuGet.Configuration
{
    internal class GreetClientConfig
    {
        private string _address;
        public string Address => _address ?? (_address = GetString(nameof(Address)));


        private int _insecurePort;
        public int InsecurePort => _insecurePort == 0 ? (_insecurePort = GetInt(nameof(InsecurePort))) : _insecurePort;


        private int _securePort;
        public int SecurePort => _securePort == 0 ? (_securePort = GetInt(nameof(SecurePort))) : _securePort;


        private StoreLocation? _storeLocation;
        public StoreLocation? StoreLocation => _storeLocation ?? (_storeLocation = GetEnum<StoreLocation>(nameof(StoreLocation)));

        private string _certSubjectName;
        public string CertSubjectName => _certSubjectName ?? (_certSubjectName = GetString(nameof(CertSubjectName)));


        private string GetString(string key)
        {

            var value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception($"Configuration AppSettings string value not found for the key '{key}'");
            }
            return value;
        }

        private int GetInt(string key)
        {
            var strValue = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(strValue) || !int.TryParse(strValue, out int value))
            {
                throw new Exception($"Configuration AppSettings Int32 value not found for the key '{key}'");
            }
            return value;
        }

        private TEnum GetEnum<TEnum>(string key) where TEnum : struct
        {
            var strValue = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(strValue) || !Enum.TryParse(strValue, out TEnum enumValue))
            {
                throw new Exception($"Configuration AppSettings Int32 value not found for the key '{key}'");
            }
            return enumValue;
        }
    }
}
