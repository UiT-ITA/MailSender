# Sending mails through Graph API, using .NET8
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
To support my testclient, a secret.json file, application.json or application.development.json is required.  
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
