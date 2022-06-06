using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PFEmvc.Models.Enums
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        public EmailSender(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message)
        {
            var mailMessage = new MailMessage(fromAddress, toAddress, subject, message);
            using (var client = new SmtpClient(_config["SMTP:Host"], int.Parse(_config["SMTP:Port"]))
            {
                Credentials = new NetworkCredential(_config["SMTP:Username"], _config["SMTP:Password"])
            })
            {
                await client.SendMailAsync(mailMessage);
            }
        }
        public static void SendMail(string emailbody,string email)
        {
            MailMessage mailMessage = new MailMessage("mohamedhabibapex@gmail.com", email);
            mailMessage.Subject = "Confirm your Account";
            mailMessage.Body = emailbody;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "mohamedhabibapex@gmail.com",
                Password = "pihmhigucvtxxnqj"
            };
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
            
        }
        public static void SendPasswordResetEmail(string emailbody, string email)
        {
            // MailMessage class is present is System.Net.Mail namespace
            MailMessage mailMessage = new MailMessage("mohamedhabibapex@gmail.com", email);
            mailMessage.Body = emailbody;
            mailMessage.Subject = "Reset Your Password";
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "mohamedhabibapex@gmail.com",
                Password = "pihmhigucvtxxnqj"
            };

            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }
    }
}
