using System.Net;
using System.Net.Mail;

namespace Careers.Services
{
    public class EmailService
    {
        public void SendEmail(string email, string subject, string message)
        {

            var mail = new MailMessage {Subject = subject, Body = message};
            mail.To.Add(email);
            mail.From = new MailAddress("sayrus719.noreply@gmail.com");

            var smtpServer = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("sayrus719.noreply@gmail.com", "P@ssword123456"),
                EnableSsl = true
            };

            smtpServer.Send(mail);
        }
    }
}
