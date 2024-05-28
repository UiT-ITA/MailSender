using Microsoft.Graph;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Azure.Identity;
namespace UiT.MailSenderComponentDI.Extensions;

public static class GraphConfiguration
{
    public static IServiceCollection ConfigureGraphComponent(this IServiceCollection services, IConfiguration configuration)
    {
        // Read configuration settings from appsettings.json
        string[] scopes = new string[] { "https://graph.microsoft.com/.default" }; //This scope is required 
        var options = new TokenCredentialOptions { AuthorityHost = AzureAuthorityHosts.AzurePublicCloud };
        string tenantId = configuration["SmtpUsers:TenantId"] ?? string.Empty;
        string clientId = configuration["SmtpUsers:ClientId"] ?? string.Empty;
        string clientSecret = configuration["SmtpUsers:ClientSecret"] ?? string.Empty;

        var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret, options);
        GraphServiceClient graphServiceClient = new GraphServiceClient(clientSecretCredential, scopes);

        // Register the GraphServiceClient with DI
        services.AddTransient<GraphServiceClient>(provider =>
        {
            return new GraphServiceClient(clientSecretCredential, scopes);
        });

        return services;
    }
}
