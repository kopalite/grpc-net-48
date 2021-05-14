using System;
using System.Collections.Generic;
using System.Configuration;

namespace TestGrpc.Client.NuGet
{
    internal class ConfigReader
    {
        public string InsecureAddress { get; private set; }
        public int InsecurePort { get; private set; }
        public string SecureAddress { get; private set; }
        public int SecurePort { get; private set; }
        public string CertSubjectName { get; internal set; }

        public ConfigReader()
        {
            InsecureAddress = GetString(nameof(InsecureAddress));
            InsecurePort = GetInt(nameof(InsecurePort));
            SecureAddress = GetString(nameof(SecureAddress));
            SecurePort = GetInt(nameof(SecurePort));
            CertSubjectName = GetString(nameof(CertSubjectName));
        }

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
    }
}
