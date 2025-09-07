using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using TravelDesk_Api.Models;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_configuration["Smtp:SenderName"], _configuration["Smtp:SenderEmail"]));
        email.To.Add(new MailboxAddress("", toEmail));
        email.Subject = subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = body;
        email.Body = builder.ToMessageBody();

        using (var smtp = new SmtpClient())
        {
            await smtp.ConnectAsync(_configuration["Smtp:Server"], int.Parse(_configuration["Smtp:Port"]), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration["Smtp:Username"], _configuration["Smtp:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}