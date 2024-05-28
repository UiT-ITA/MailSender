using Microsoft.Graph.Models;

namespace UiT.MailSenderComponentDI.Services
{
    public interface ISMTPService
    {
        public Message MakeMessage(string torecipient, string subject, string body);
        public Message MakeMessage(string[] torecipients, string subject, string body);
        public Message MakeMessage(string[] torecipients, string[] ccrecipients, string[] bccrecipients, string subject, string body);
        public Message MakeMessage(string[] torecipients, string[] ccrecipients, string[] bccrecipients, string subject, string body, BodyType bodyType);
        public Message MakeMessage(string torecipient, string subject, string body, BodyType bodyType);
        public Task SendMailUsers(string senderUpn, Message message);
        public Task SendMailUsers(string senderUpn, Message message, bool SaveToSent);
    }
}