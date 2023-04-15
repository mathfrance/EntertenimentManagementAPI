namespace EntertenimentManager.API
{
    public static class Configuration
    {
        public static string JwtKey { get; set; }
        public static string ApiKeyName { get; set; }
        public static string ApiKey { get; set; }
        public static string AzureStorageConnectionString { get; set; }
        public static string ImageContainer { get; set; }

        public static SmtpConfiguration Smtp { get; set; }

        public class SmtpConfiguration
        {
            public string Host { get; set; }
            public int Port { get; set; } = 25;
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}
