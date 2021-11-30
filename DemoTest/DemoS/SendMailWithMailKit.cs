using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoTest.DemoS
{
    public class SendMailWithMailKit
    {
        public static void SendEmailProcess()
        {
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("Easyrewardz", "info@easyrewardz.com"));
			message.To.Add(new MailboxAddress("Aashish", "aashish@easywardz.com"));
			message.Subject = "How you doin'?";
			message.Body = new TextPart("plain")
			{
				Text = @"Hey Chandler, I just wanted to let you know that Monica and I were going to go play some paintball, you in? -- Joey"
			};

            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp.socketlabs.com", 587, false);
                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("server21138", "b9W8Bxm4N3Zej5R6Ftw");
                client.Send(message);
                client.Disconnect(true);
            }
		}
    }
}
