using System;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace MobileCore.Email.Droid
{
    public class EmailService : IEmailService
    {
        public async Task SendErrorEmail(string title, string message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(title, "coffeemanager221@gmail.com"));
            email.To.Add(new MailboxAddress("", "tertyshnykov@gmail.com"));
            email.Subject = "Error";

            email.Body = new TextPart("plain")
            {
                Text = message
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect("smtp.gmail.com", 587, false);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("coffeemanager221@gmail.com", "Q!W@E#R$");

                    await client.SendAsync(email);
                    client.Disconnect(true);
                }
            }
            catch(Exception ex)
            {
                
            }
           
        }
    }
}
