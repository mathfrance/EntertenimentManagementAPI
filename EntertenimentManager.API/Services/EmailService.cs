using System.Net;
using System.Net.Mail;
namespace EntertenimentManager.API.Services
{
    public class EmailService
    {
        public bool Send(
            string toName,
            string toEmail,
            string subject,
            string body,
            string fromName = "Gerenciador de entretenimento",
            string fromEmail = "GerenciadorDeEntretenimento@outlook.com")
        {
            var smtpClient = new SmtpClient(Configuration.Smtp.Host, Configuration.Smtp.Port)
            {
                Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mail.To.Add(new MailAddress(toEmail, toName));

            try
            {
                smtpClient.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
