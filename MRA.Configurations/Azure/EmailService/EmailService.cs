﻿using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MRA.Configurations.Common.Constants;
using MRA.Configurations.Common.Interfaces.Services;
using System.Net;

namespace MRA.Configurations.Azure.EmailService;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(IEnumerable<string> receives, string body, string subject)
    {
        var protocol = ServicePointManager.SecurityProtocol;
        try
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;
            var connectionString = _configuration[ConfigurationKeys.AZURE_EMAIL_CONNECTION]; // Find your Communication Services resource in the Azure portal
            var _client = new EmailClient(connectionString);
            // Create the email content
            var emailContent = new EmailContent(subject) { Html = body };

            // Create the recipient list
            var emailRecipients = new EmailRecipients(receives.Select(s => new EmailAddress(s)));

            // Create the EmailMessage
            var emailMessage = new EmailMessage(
                _configuration[ConfigurationKeys.AZURE_EMAIL_SENDER],
                emailRecipients, emailContent);

            try
            {
                await _client.SendAsync(WaitUntil.Started, emailMessage);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on sending email");
                return false;
            }
        }
        finally
        {
            ServicePointManager.SecurityProtocol = protocol;
        }
    }
}