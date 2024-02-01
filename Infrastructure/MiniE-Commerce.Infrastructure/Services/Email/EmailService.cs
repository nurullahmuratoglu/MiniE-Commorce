using MediatR;
using Microsoft.Extensions.Options;
using MiniE_Commorce.Application.BaseAppSettings;
using MiniE_Commorce.Application.Interfaces.Services.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MiniE_Commerce.Infrastructure.Services.Email
{
    public class EmailService : IEmailService
    {
        private EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> option)
        {
            _emailSettings = option.Value;
        }


        public async Task SendEmailAsync(string email, string body, string subject = null)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Host = _emailSettings.Host;
                smtpClient.Port = _emailSettings.Port;
                smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.Email),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true // Varsayılan olarak HTML olarak gönderim yapılacaksa
                };
                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }

        }
    }
}
