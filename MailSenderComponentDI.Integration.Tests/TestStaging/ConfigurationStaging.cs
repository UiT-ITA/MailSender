using Microsoft.Extensions.Configuration;
using UiT.MailSenderComponentDI.Integration.Tests.Services;

namespace UiT.MailSenderComponentDI.Integration.Tests.TestStaging
{
    public static class ConfigurationStaging
    {
        public static IConfiguration GetConfiguration(string environment)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddUserSecrets<SMTPServiceTests>()
                .Build();
            return config;
        }
        public static void AddToConfiguration(string environment, IConfigurationBuilder config)
        {
            config.AddJsonFile($"appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddUserSecrets<SMTPServiceTests>()
                .Build();
        }
    }
}
