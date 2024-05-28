using UiT.MailSenderComponentDI.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using UiT.MailSenderComponentDI.Services;

namespace UiT.MailSenderComponentDI.Integration.Tests.TestStaging;

public static class HostBuilderStaging
{
    public static IHost GetHost(string environment)
    {
        // Create a HostBuilder
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                ConfigurationStaging.AddToConfiguration(environment, config);                    
            })
            .ConfigureLogging((hostContext, configLogging) =>
            {
                configLogging.ClearProviders();
                configLogging.AddConsole();
                configLogging.AddDebug();
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.ConfigureGraphComponent(hostContext.Configuration);
                services.AddTransient<ISMTPService, SMTPService>();
            })
            .Build();
        return host;
    }    
}
