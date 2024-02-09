// See https://aka.ms/new-console-template for more information
using MailSenderCli;
using SMTPClient;

Console.WriteLine("Hello, World!");
var secretAppsettingReader = new SecretAppsettingReader();
var secrets1 = secretAppsettingReader.ReadSection<Secrets>("SmtpMe");
var secrets2 = secretAppsettingReader.ReadSection<Secrets>("SmtpUsers");

// If you want to use "Me" as sender
MailSenderComponent.SMTPClient smtpclient1 = new MailSenderComponent.SMTPClient(secrets1);
await smtpclient1.GetGraphServiceClientMe();
var message1 = MailSenderComponent.SMTPClient.MakeMessage("you@gmail.com", "Test", "Sent by <b>Me</b>");
await smtpclient1.SendMailMe(message1);


// If you want have "whoever/user" as sender
MailSenderComponent.SMTPClient smtpclient2 = new MailSenderComponent.SMTPClient(secrets2);
await smtpclient2.GetGraphServiceClientUsers();

// Still sends as you. But no device login... 
var message2 = MailSenderComponent.SMTPClient.MakeMessage("you@gmail.com", "Test", "Sent by <b>Users</b>");
await smtpclient2.SendMailUsers("your@domain.com", message2); 

// Sends as noreply@yourdomain.com
var message3 = MailSenderComponent.SMTPClient.MakeMessage("you@gmail.com", "Test", "Sent by <b>Users</b> fra NoReply");
await smtpclient2.SendMailUsers("noreply@yourdomain.com", message3);

