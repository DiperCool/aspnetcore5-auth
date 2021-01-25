using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Web.Infrastructure.EmailSender
{
    public class EmailSender : IEmailSender
    {
        IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendConfirmEmail(string email, string url)
        {
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress(_configuration["MailKit:name"],_configuration["MailKit:email"]);
            message.From.Add(from);
            MailboxAddress to = MailboxAddress.Parse(email);
            message.To.Add(to);
            message.Subject = "Confirm your email";
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = string.Format("<a href='{0}'>confirm</a>", url);
            message.Body = bodyBuilder.ToMessageBody();
            SmtpClient client = new SmtpClient();
            await client.ConnectAsync(_configuration["MailKit:smtp"], Int32.Parse(_configuration["MailKit:port"]));
            await client.AuthenticateAsync(_configuration["MailKit:email"], _configuration["MailKit:password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}