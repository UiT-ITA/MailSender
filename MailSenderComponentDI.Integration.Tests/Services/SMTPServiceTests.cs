﻿using Microsoft.Extensions.DependencyInjection;
using UiT.MailSenderComponentDI.Integration.Tests.TestStaging;
using UiT.MailSenderComponentDI.Services;
using Microsoft.Graph.Models;

namespace UiT.MailSenderComponentDI.Integration.Tests.Services;

public class SMTPServiceTests
{
    [Theory]
    [InlineData("espen.rivedal@uit.no", "espen.rivedal@uit.no", "Dette er en test", "Dette er body text linje1")]
    public async Task SendMailUsersTest(string toAddress,
                                        string fromAddress,
                                        string subject,
                                        string bodyContent)
    {
        // Arrange
        var host = HostBuilderStaging.GetHost("Development");
        var configuration = ConfigurationStaging.GetConfiguration("Development");
        var smtpService = host.Services.GetRequiredService<ISMTPService>();

        var message = smtpService.MakeMessage(toAddress, subject, bodyContent, BodyType.Text);

        // Act
        await smtpService.SendMailUsers(fromAddress, message);

        // Assert
        // If no exception was thrown assume success
        // There is no way to check if the mail actually arrived
    }
}
