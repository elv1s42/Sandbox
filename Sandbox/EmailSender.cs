using System.Net;
using System.Net.Mail;

namespace Sandbox
{
    public static class EmailSender
    {
        public static void Test()
        {
        }

        public static void Send()
        {
            var fromAddress = new MailAddress("Ololo@test.test");
            var toAddress = new MailAddress("Test@test.ru", "Testing name");
            const string fromPassword = "123";
            const string subject = "Subject";
            const string body = "Body";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("Ololo", fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
        
    }
}
