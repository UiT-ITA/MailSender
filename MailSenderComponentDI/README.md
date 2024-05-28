## Project description

Dependency injection version of MailSenderComponent project.

Currently only supports sending email as application.

Later we may add sending as logged in user.

### Usage

Add GraphComponent and SMTPService in Program.cs

```csharp
    services.ConfigureGraphComponent(hostContext.Configuration);
    services.AddTransient<ISMTPService, SMTPService>();
```

Then inject in one of you services

```csharp
    private readonly ISMTPService _smtpService;

    public MyService(ISMTPService smtpService)
    {
        _smtpService = smtpService;
    }
```

Use the service

```csharp
    var message = SMTPService.MakeMessage(toAddress, subject, body);
    await smtpService.SendMailUsers(fromAddress, message);
```

### Secrets

User secrets must be set for this project to work.

```json
{
  "SmtpUsers": {
    "TenantId": "xxxxx",
    "AppName": "Anonymous Email Sender",
    "ClientId": "xxxxx",
    "ClientSecret": "xxxxx"
  }
```

In azure AD , the app registration must have the following API permissions:

- Microsoft Graph
  - Mail.Send  

This must be granted by an admin.

## Developers

espen.rivedal@uit.no