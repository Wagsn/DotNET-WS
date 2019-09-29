using System;
using System.Net;
using System.Net.Mail;

namespace WS.DotNetCore.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // MailKit / MimeKit
            // 邮箱授权码：lmiuqpvhlnmebcfe
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("wagsn@foxmail.com", "Wagsn");
            mailMessage.To.Add(new MailAddress("vsm2464@dingtalk.com"));
            //var bodyBuilder = new BodyBuilder();
            //mailMessage.To.Add(new MailAddress("774284673@qq.com"));
            mailMessage.Subject = ".NET Core 邮件发送测试";
            //mailMessage.Body = "纯文本测试第一行\r\n纯文本测试第二行";
            mailMessage.Body = "<h1>富文本标题</h1><p>富文本正文</p>";
            mailMessage.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.qq.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("wagsn@foxmail.com", "lmiuqpvhlnmebcfe");

            // 发送
            try
            {
                client.Send(mailMessage);
            }
            catch(Exception e)
            {
                Console.Error.WriteLine($"Error: {e}");
            }
            Console.WriteLine("邮件已发送");
            Console.ReadKey();
        }
    }
}
