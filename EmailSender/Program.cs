namespace EmailSender
{
    class Program
    {
        static void Main(string[] args)
        {
            // Mailbox Authorization Code: lmiuqpvhlnmebcfe

            // MailKit / MimeKit
            var message = new MimeKit.MimeMessage();
            message.From.Add(new MimeKit.MailboxAddress("Wagsn", "wagsn@foxmail.com"));
            message.To.Add(new MimeKit.MailboxAddress("2464", "vsm2464@dingtalk.com"));

            message.Subject = "星期天去哪里玩？";
            var bodyBuilder = new MimeKit.BodyBuilder();
            //bodyBuilder.HtmlBody = @"<h1>计划</h1><p>我想去故宫玩，如何</p>";
            bodyBuilder.HtmlBody = "计划\r\n我想去故宫玩，如何";
            message.Body = bodyBuilder.ToMessageBody();
            //message.Body = new TextPart("计划") { Text = "我想去故宫玩，如何" };

            using (var client2 = new MailKit.Net.Smtp.SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client2.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client2.Connect("smtp.qq.com", 587, false);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client2.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client2.Authenticate("wagsn@foxmail.com", "lmiuqpvhlnmebcfe");

                client2.Send(message);
                client2.Disconnect(true);
            }

            //System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            //mailMessage.From = new System.Net.Mail.MailAddress("wagsn@foxmail.com", "Wagsn");
            //mailMessage.To.Add(new System.Net.Mail.MailAddress("vsm2464@dingtalk.com"));
            ////var bodyBuilder = new BodyBuilder();
            ////mailMessage.To.Add(new MailAddress("774284673@qq.com"));
            //mailMessage.Subject = ".NET Core 邮件发送测试";
            ////mailMessage.Body = "纯文本测试第一行\r\n纯文本测试第二行";
            //mailMessage.Body = "<h1>富文本标题</h1><p>富文本正文</p>";
            //mailMessage.IsBodyHtml = true;

            //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            //client.Host = "smtp.qq.com";
            //client.Port = 587;
            //client.EnableSsl = true;
            //client.Credentials = new NetworkCredential("wagsn@foxmail.com", "lmiuqpvhlnmebcfe");

            //try
            //{
            //    client.Send(mailMessage);
            //}
            //catch(Exception e)
            //{
            //    Console.Error.WriteLine($"Error: {e}");
            //}

            System.Console.WriteLine("邮件已发送");
            System.Console.ReadKey();
        }
    }
}
