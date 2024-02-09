using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ODataErrors;
using SMTPClient;
using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MailSenderComponent;

public class SMTPClient
{
    private string TenantId { get; set; } = string.Empty;
    private string ClientId { get; set; } = string.Empty;
    private string ClientSecret { get; set; } = string.Empty;
    GraphServiceClient GraphServiceClient { get; set; } = null!;

    public SMTPClient(string TenantId, string ClientId, string ClientSecret)
    {
        this.TenantId = TenantId;
        this.ClientId = ClientId;
        this.ClientSecret = ClientSecret;
    }

    public SMTPClient(Secrets secrets)
    {
        this.TenantId = secrets.TenantId;
        this.ClientId = secrets.ClientId;
        this.ClientSecret = secrets.ClientSecret;
    }



    public async Task GetGraphServiceClientMe()
    {
        var scopes = new[] { "Mail.Send" }; // "Mail.Read", "Mail.Send", "profile", "User.Read" }; // All of the listed
        Console.WriteLine("Open the link in text, copy code from browser, log in as desired user.");

        var options = new DeviceCodeCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            ClientId = ClientId,
            TenantId = TenantId,

            // Callback function that receives the user prompt
            // Prompt contains the generated device code that user must
            // enter during the auth process in the browser
            DeviceCodeCallback = (code, cancellation) =>
            {
                Console.WriteLine(code.Message);
                return Task.FromResult(0);
            },
        };
        // https://learn.microsoft.com/dotnet/api/azure.identity.devicecodecredential
        var deviceCodeCredential = new DeviceCodeCredential(options);
        GraphServiceClient = new GraphServiceClient(deviceCodeCredential, scopes);
        var me = await GraphServiceClient.Me.GetAsync(); //.Result.DisplayName;
        if (me is null) throw new Exception("No user found");
        Console.WriteLine($"Logged in as: {me.UserPrincipalName}, {me.DisplayName}, {me.Mail ?? ""}");
    }

    public async Task GetGraphServiceClientUsers()
    {        
        string[] scopes = new string[] { "https://graph.microsoft.com/.default" }; //This scope is required 
        var options = new TokenCredentialOptions{ AuthorityHost = AzureAuthorityHosts.AzurePublicCloud };
        var clientSecretCredential = new ClientSecretCredential(TenantId, ClientId, ClientSecret, options);
        GraphServiceClient = new GraphServiceClient(clientSecretCredential, scopes);
    }


    public static Message MakeMessage(string torecipient, string subject, string body) => MakeMessage(new string[1] { torecipient }, new string[0], new string[0], subject, body, BodyType.Html);
    public static Message MakeMessage(string[] torecipients, string subject, string body) => MakeMessage(torecipients, new string[0], new string[0], subject, body, BodyType.Html);
    public static Message MakeMessage(string[] torecipients, string[] ccrecipients, string[] bccrecipients, string subject, string body) => MakeMessage(torecipients, ccrecipients, bccrecipients, subject, body, BodyType.Html);
    public static Message MakeMessage(string[] torecipients, string[] ccrecipients, string[] bccrecipients, string subject, string body, BodyType bodyType)
    {
        // Recipients
        List<Recipient> toRecipients = new();
        foreach (string recipient in torecipients)
        {
            toRecipients.Add(new Recipient { EmailAddress = new EmailAddress { Address = recipient } });
        }

        List<Recipient> ccRecipients = new();
        foreach (string recipient in ccrecipients)
        {
            ccRecipients.Add(new Recipient { EmailAddress = new EmailAddress { Address = recipient } });
        }

        List<Recipient> bccRecipients = new();
        foreach (string recipient in bccrecipients)
        {
            bccRecipients.Add(new Recipient { EmailAddress = new EmailAddress { Address = recipient } });
        }

        // Define a simple e-mail message.
        var message = new Message
        {
            Subject = subject,
            Body = new ItemBody
            {
                ContentType = bodyType,
                Content = body
            },
            ToRecipients = toRecipients,
            CcRecipients = ccRecipients,
            BccRecipients = bccRecipients
        };

        return message;
    }



    public async Task SendMailMe(Message message) => await SendMailMe(message, false);
    public async Task SendMailMe(Message message, bool SaveToSent)
    {
        try
        {
            Microsoft.Graph.Me.SendMail.SendMailPostRequestBody postitem = new()
            {
                Message = message,
                SaveToSentItems = SaveToSent
            };

            // Send mail as the given user. 
            await GraphServiceClient.Me.SendMail.PostAsync(postitem);
        }
        catch(ODataError odataError)
        {
            Console.WriteLine(odataError.Message);
            throw;
        }
        catch(Exception exc)
        {
            Console.WriteLine(exc.Message);
            throw;
        }

    }


    public async Task SendMailUsers(string senderUpn, Message message) => await SendMailUsers(senderUpn, message, false);
    public async Task SendMailUsers(string senderUpn, Message message, bool SaveToSent)
    {
        try
        {
            Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody postitem = new()
            {
                Message = message,
                SaveToSentItems = SaveToSent
            };

            // Send mail as the given user. 
            await GraphServiceClient.Users[senderUpn].SendMail.PostAsync(postitem);
        }
        catch (ODataError odataError)
        {
            Console.WriteLine(odataError.Message);
            throw;
        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.Message);
            throw;
        }

    }



}
