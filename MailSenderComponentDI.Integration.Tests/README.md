## Project description

Test project for MailSenderComponentDI project.
Use this to test sending email.
Note this is kind of manual test because after running test send
you must check your email to see if email was sent.

Tests that the dependency injection is working correctly.

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

To get test secrets look in 1password in entry "Email Sender"

## Developers

espen.rivedal@uit.no