// See https://aka.ms/new-console-template for more information
using MailSenderCli;
using SMTPClient;

Console.WriteLine("Hello, World!");
var secretAppsettingReader = new SecretAppsettingReader();
var secrets1 = secretAppsettingReader.ReadSection<Secrets>("SmtpMe");
var secrets2 = secretAppsettingReader.ReadSection<Secrets>("SmtpUsers");


//MailSenderComponent.SMTPClient smtpclient1 = new MailSenderComponent.SMTPClient(secrets1);
//await smtpclient1.GetGraphServiceClientMe();
//var message1 = MailSenderComponent.SMTPClient.MakeMessage("Raymond.Andreassen@gmail.com", "Test", "Sent gjennom <b>Me</b>");
//await smtpclient1.SendMailMe(message1);


// Anonymous Email Sender
MailSenderComponent.SMTPClient smtpclient2 = new MailSenderComponent.SMTPClient(secrets2);
await smtpclient2.GetGraphServiceClientUsers();
var message2 = MailSenderComponent.SMTPClient.MakeMessage("Raymond.Andreassen@gmail.com", "Test", "Sent gjennom <b>Users</b>");
await smtpclient2.SendMailUsers("ran039@uit.no", message2);


var message3 = MailSenderComponent.SMTPClient.MakeMessage("Raymond.Andreassen@gmail.com", "Test", "Sent gjennom <b>Users</b> fra NoReply");
await smtpclient2.SendMailUsers("NoReplyAutomation@uit.no", message3);

