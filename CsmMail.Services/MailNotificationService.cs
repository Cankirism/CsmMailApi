using CsmMail.Dto;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CsmMail.Services
{
    public class MailNotificationService : INotificationService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _username;
        private readonly string _password;
        private readonly string _toAddress;
        public MailNotificationService(IConfiguration config)
        {
            _smtpHost = config.GetSection("MailInfo").GetSection("SmtpHost").Value;
            _smtpPort = int.Parse(config.GetSection("MailInfo").GetSection("SmtpPort").Value);
            _username = config.GetSection("MailInfo").GetSection("Username").Value;
            _password = config.GetSection("MailInfo").GetSection("Password").Value;
            _toAddress = config.GetSection("MailInfo").GetSection("ToAddress").Value;

        }


        public async Task<string> SendAsync(MailDto dto)
        {
            try
            {
                using (var client = new SmtpClient(_smtpHost, _smtpPort))
                {

                    client.UseDefaultCredentials = false;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_username, _password);

                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(_username);
                    var addresses = _toAddress.Split(";");
                    foreach (var address in addresses)
                    {
                        mail.To.Add(address);
                    }
                  
                    mail.Subject = dto.Subject;
                    mail.Body = dto.Body;
                    mail.IsBodyHtml = true;
                    await client.SendMailAsync(mail);
                   

                    return "success";
                }
            }

            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
