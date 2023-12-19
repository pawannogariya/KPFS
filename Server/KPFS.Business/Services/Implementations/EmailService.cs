using KPFS.Business.Models;
using KPFS.Business.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace KPFS.Business.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfigurationDto emailConfig;
        private readonly ILogger<EmailService> logger;
        public EmailService(EmailConfigurationDto emailConfig, ILogger<EmailService> logger)
        {
            this.emailConfig = emailConfig;
            this.logger = logger;
        }
        public void SendEmail(MessageDto message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(MessageDto message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("KPFS EXP", emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message.Content };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(emailConfig.SmtpServer, emailConfig.Port, true);//MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(emailConfig.UserName, emailConfig.Password);

                client.Send(mailMessage);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
