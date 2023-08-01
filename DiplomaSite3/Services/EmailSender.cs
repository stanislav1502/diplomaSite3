using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace DiplomaSite3.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string message, string subject = "Confirm your email")
        {
            // throw new NotImplementedException();

            var mailUser = WebApplication.CreateBuilder().Configuration["mailSender:username"];
            var mailPass = WebApplication.CreateBuilder().Configuration["mailSender:password"];

            SmtpClient client = new SmtpClient
            {
                Port = 443,
                Host = "smtp.gmail.com", //or another email sender provider
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mailUser, mailPass)
            };

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("thesis@project.com");
            mailMessage.To.Add(new MailAddress(email));

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = message;

            return client.SendMailAsync(mailMessage);

        }
    }
}
