using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SmartAdmin.WebUI.Models
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;     
        public EmailSender(
            IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }
        async Task Execute(string email, string subject, string htmlMessage)
        {
            SmtpClient client = new SmtpClient(_emailSettings.MailServer, _emailSettings.MailPort);
            try
            {
                client.Timeout = 0;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_emailSettings.Sender, _emailSettings.Password);
                MailMessage mailMessage = new MailMessage(_emailSettings.Sender, email, subject, htmlMessage);
                await client.SendMailAsync(mailMessage);
            }
            catch (SmtpException ex)
            {
                throw;
            }
        }

        
    }

}
