using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Security;



namespace DiplomaSite3.Services
{
    public class MailSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class MailData
    {
        public string EmailToId { get; set; }
        public string EmailToName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
    }

    public interface IEmailService
    {
        Task<bool> SendMailAsync(MailData mailData, CancellationToken ct);
    }

    public class MyEmailSender : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public MyEmailSender(IOptions<MailSettings> mailSettingsOptions)
        {
            _mailSettings = mailSettingsOptions.Value;
        }

        public async Task<bool> SendMailAsync(MailData mailData, CancellationToken ct)
        {

            using (MimeMessage emailMessage = new MimeMessage())
            {
                MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
                emailMessage.From.Add(emailFrom);
                MailboxAddress emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
                emailMessage.To.Add(emailTo);

                emailMessage.Subject = mailData.EmailSubject;

                BodyBuilder emailBodyBuilder = new BodyBuilder();
                emailBodyBuilder.HtmlBody = mailData.EmailBody;

                emailMessage.Body = emailBodyBuilder.ToMessageBody();

                var mailClient = new SmtpClient();
                try
                {
                    await mailClient.ConnectAsync(_mailSettings.Server, _mailSettings.Port, SecureSocketOptions.SslOnConnect, ct);
                    await mailClient.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password, ct);
                    await mailClient.SendAsync(emailMessage,ct);
                    await mailClient.DisconnectAsync(true, ct);
                }
                catch (Exception ex)
                {
                    // Exception Details
                    return false;
                }
                finally
                {
                    mailClient.Dispose();
                }

                return true;
            }

        }
    }
}