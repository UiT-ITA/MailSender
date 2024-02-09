# Sending mails through Graph API, using .NET8

## Usage 
### Constructor
SMTPClient(Secrets secrets) 
SMTPClient(string TenantId, string ClientId, string ClientSecret)
### Get GraphClient
async Task GetGraphServiceClientMe()  // Get for ME
async Task GetGraphServiceClientUsers() // Get for USERS
### Make message
static Message MakeMessage(string torecipient, string subject, string body) // Html, single recipient
static Message MakeMessage(string[] torecipients, string subject, string body) // Html, multiple recipients
static Message MakeMessage(string[] torecipients, string[] ccrecipients, string[] bccrecipients, string subject, string body) // Html, multiple to, cc, bcc
static Message MakeMessage(string[] torecipients, string[] ccrecipients, string[] bccrecipients, string subject, string body, BodyType bodyType) // multiple to, cc, bcc
### Send message
async Task SendMailMe(Message message) // Sent by ME
async Task SendMailMe(Message message, bool SaveToSent) // Sent by ME, save it to sent items
async Task SendMailUsers(string senderUpn, Message message) // Sent by senderUpn
async Task SendMailUsers(string senderUpn, Message message, bool SaveToSent)// Sent by senderUpn, save it to sent items

## Type 1: Me
Sending as logged in user. Requires CODE login since it uses DeviceCodeCredential to connect to your user account. 
Azure App setup: 
* Register app, name, etc.
* Api permission: Mail.Send, delegated. Grant admin concent.
* Create secret
* Authentication -> Allow public client flows (enable)

## Type 2: Any user
Sending as typed username (upn or objectId). Requires that the "target" user has an mailbox. 
Azure App setup: 
* Register app, name, etc. 
* Api permission: Mail.Send, application. Grant admin concent. 
* Create secret. 

## Secrets file
To support my testclient
- secret.json file
- application.json
- application.development.json  

One of them is required.  

```
{
  "SmtpMe": {
    "TenantId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    "AppName": "Personal Email Sender",
    "ClientId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    "ClientSecret": "yoursecret"
  },
  "SmtpUsers": {
    "TenantId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
    "AppName": "Anonymous Email Sender",
    "ClientId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",,
    "ClientSecret": "yoursecret"
  }
}
```
