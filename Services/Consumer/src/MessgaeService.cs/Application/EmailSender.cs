using System.Net.Mail;
using System.Net;
using MessageService.Models;
using MessageService.Abstractions;
using Microsoft.Extensions.Options;

namespace MessageService.Application
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfig config;
        public EmailSender(IOptions<EmailConfig> config)
            => this.config = config.Value ?? throw new ArgumentNullException(nameof(config));
           
        public bool Send(MailMessage message)
           => SendHandler(message, new CancellationTokenSource().Token);
        private bool SendHandler(MailMessage message, CancellationToken cancellationToken)
        {
            using (var client = new SmtpClient(config.SmtpServer, config.Port))
            {
                try
                {
                    message.From = new MailAddress(config.From);
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(config.UserName, config.Password);

                    client.SendAsync(message, cancellationToken);
                }
                catch
                {
                    return false;
                }
                finally
                {
                    client.Dispose();
                }
                return true;
            }
        }
    }
}
