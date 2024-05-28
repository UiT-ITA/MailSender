using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ODataErrors;

namespace UiT.MailSenderComponentDI.Services;

public class SMTPService : ISMTPService
{
    private readonly GraphServiceClient _graphServiceClient;

    public SMTPService(GraphServiceClient graphServiceClient)
    {
        _graphServiceClient = graphServiceClient;
    }

    public Message MakeMessage(string torecipient, string subject, string body) => MakeMessage(new string[1] { torecipient }, new string[0], new string[0], subject, body, BodyType.Html);
    public Message MakeMessage(string torecipient, string subject, string body, BodyType bodyType) => MakeMessage(new string[1] { torecipient }, new string[0], new string[0], subject, body, bodyType);
    public Message MakeMessage(string[] torecipients, string subject, string body) => MakeMessage(torecipients, new string[0], new string[0], subject, body, BodyType.Html);
    public Message MakeMessage(string[] torecipients, string[] ccrecipients, string[] bccrecipients, string subject, string body) => MakeMessage(torecipients, ccrecipients, bccrecipients, subject, body, BodyType.Html);
    public Message MakeMessage(string[] torecipients, string[] ccrecipients, string[] bccrecipients, string subject, string body, BodyType bodyType)
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
            await _graphServiceClient.Users[senderUpn].SendMail.PostAsync(postitem);
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
