namespace EmailSender
{
    /// <summary>
    /// 电子邮件帮助器
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        /// 通过默认邮箱发送邮件
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="htmlBody">HTML邮件体</param>
        /// <param name="toName">接收名称</param>
        /// <param name="toAddress">接收邮箱</param>
        public static void Send(string subject, string htmlBody, string toName, string toAddress)
        {
            // Mailbox Authorization Code: lmiuqpvhlnmebcfe

            // MailKit / MimeKit
            var message = new MimeKit.MimeMessage();
            var defaultFrom = new MimeKit.MailboxAddress("Wagsn", "wagsn@foxmail.com");
            message.From.Add(defaultFrom);
            //message.To.Add(new MimeKit.MailboxAddress("2464", "vsm2464@dingtalk.com"));
            message.To.Add(new MimeKit.MailboxAddress(toName, toAddress));

            //message.Subject = "星期天去哪里玩？";
            message.Subject = subject;
            var bodyBuilder = new MimeKit.BodyBuilder();
            //bodyBuilder.HtmlBody = @"<h1>计划</h1><p>我想去故宫玩，如何</p>";
            //bodyBuilder.TextBody = "";
            bodyBuilder.HtmlBody = htmlBody;
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
        }
    }
}
