﻿using System;
using CoffeManager.Common;
using MailKit.Net.Smtp;
using MimeKit;

namespace CoffeeManager.Droid
{
    public class EmailService : IEmailService
    {
        public void SendErrorEmail(string message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Info", "serdechnyi.dima@gmail.com"));
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
                    client.Authenticate("coffeemanager221@gmail.com", "Q!w2e3r4");

                    client.Send(email);
                    client.Disconnect(true);
                }
            }
            catch(Exception ex)
            {
                
            }
           
        }
    }
}