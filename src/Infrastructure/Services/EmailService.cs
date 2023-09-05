using System.Net;
using System.Net.Mail;
using Application.Services;
using Domain.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;
public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailService(IOptions<SmtpSettings> smtpSettings)
    {
        _smtpSettings = smtpSettings.Value;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        try
        {
            using (var client = CreateSmtpClient())
            {
                using (var mailMessage = CreateMailMessage(toEmail, subject, message))
                {
                    await client.SendMailAsync(mailMessage);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private SmtpClient CreateSmtpClient()
    {
        var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
        {
            EnableSsl = _smtpSettings.EnableSsl,
            Timeout = _smtpSettings.Timeout,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password)
        };
        return client;
    }

    private MailMessage CreateMailMessage(string toEmail, string subject, string message)
    {
        var mailMessage = new MailMessage
        {
            To = { toEmail },
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };
        return mailMessage;
    }
}