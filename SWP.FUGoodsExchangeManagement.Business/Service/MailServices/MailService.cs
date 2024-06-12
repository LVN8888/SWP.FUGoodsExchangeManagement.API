using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Security;

namespace SWP.FUGoodsExchangeManagement.Business.Service.MailServices
{
    public class MailService : IMailService
    {
        public async Task<bool> SendEmail(string Email, string Subject, string Html)
        {
            try
            {
                var toEmail = Email;
                string from = "fugoodsexchangesystem@gmail.com";
                string pass = "nivt cpmg iobw dlsn";
                MimeMessage message = new();
                message.From.Add(MailboxAddress.Parse(from));
                message.Subject = "[GEFU] " + Subject;
                message.To.Add(MailboxAddress.Parse(toEmail));
                message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = Html
                };
                using MailKit.Net.Smtp.SmtpClient smtp = new();
                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(from, pass);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
