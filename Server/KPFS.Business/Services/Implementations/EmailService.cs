using KPFS.Business.Models;
using KPFS.Business.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace KPFS.Business.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfigurationDto _emailConfig;
        public EmailService(EmailConfigurationDto emailConfig) => _emailConfig = emailConfig;
        public void SendEmail(MessageDto message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(MessageDto message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("KPFS EXP", _emailConfig.From));
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
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);//MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                client.Send(mailMessage);
            }
            catch
            {
                //log an error message or throw an exception or both.
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
