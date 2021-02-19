using BookStore.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public class EmailService : IEmailService
    {
        private const String templatePath = @"EmailTemplates/{0}.html";
        private readonly IOptions<SMTPConfigModel> _smtpConfigMode;

        public EmailService(IOptions<SMTPConfigModel> smtpConfigMode)
        {
            _smtpConfigMode = smtpConfigMode;
        }

        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = "My Subject for email";
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("TestEmail"),userEmailOptions.Placeholders);
            await SendEmail(userEmailOptions);
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfigMode.Value.SenderAddress, _smtpConfigMode.Value.SenderDispalyName),
                IsBodyHtml = _smtpConfigMode.Value.IsBodyHTML,
                BodyEncoding = Encoding.Default

            };
            foreach (var toemail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toemail);
            }
            NetworkCredential networkCredential = new NetworkCredential(_smtpConfigMode.Value.UserName, _smtpConfigMode.Value.Password);
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfigMode.Value.Host,
                Port = _smtpConfigMode.Value.Port,
                EnableSsl = _smtpConfigMode.Value.EnableSSL,
                UseDefaultCredentials = _smtpConfigMode.Value.UseDefaultCrendentials,
                Credentials = networkCredential

            };
            await smtpClient.SendMailAsync(mail);
        }

       // private MailAddress NewMailAddress()
       // {
       //     throw new NotImplementedException();
       // }

        private static String GetEmailBody(String TemplateName)
        {
            return File.ReadAllText(String.Format(templatePath, TemplateName));
        }

        private static String UpdatePlaceHolders(String text,List<KeyValuePair<string,string>> keyValuePairs )
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
               foreach(var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                       text=text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }
            return text;
        }
    }
}
