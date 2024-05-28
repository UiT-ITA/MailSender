using Microsoft.Graph.Models;

namespace UiT.MailSenderComponentDI.Services
{
    public interface ISMTPService
    {
        public Task SendMailUsers(string senderUpn, Message message);
        public Task SendMailUsers(string senderUpn, Message message, bool SaveToSent);
    }
}