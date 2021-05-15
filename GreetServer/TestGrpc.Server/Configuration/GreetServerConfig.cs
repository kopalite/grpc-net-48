namespace TestGrpc.Server.Configuration
{
    public class GreetServerConfig
    {
        public string InsecureAddress { get; init; }
        public int InsecurePort { get; init; }
        public string SecureAddress { get; init; }
        public int SecurePort { get; init; }
        public string CertSubjectName { get; init; }
    }
}