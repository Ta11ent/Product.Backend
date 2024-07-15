using MessageService.Abstractions;
using MessageService.Models.Configuration;
using MessageService.Models.Context;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace MessageService.Application.Email
{
    public class SendEmail : ISender<EmailMessageData>
    {
        private readonly EmailConfig _config;
        private readonly IMessage<EmailMessageData, MailMessage> _message;
        public SendEmail(
            IOptions<EmailConfig> config,
            IMessage<EmailMessageData, MailMessage> messgae)
        {
            _config = config.Value ?? throw new ArgumentNullException(nameof(config));
            _message = messgae;
        }
        public void SendMessage(EmailMessageData data)
        {
            var message = _message.CreateMessage(data!);
            CancellationTokenSource cancellationToken = new();
            Send(message, cancellationToken.Token);
        }
        private void Send(MailMessage message, CancellationToken cancellationToken)
        {
            using (var client = new SmtpClient(_config.SmtpServer, _config.Port))
            {
                try
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_config.UserName, _config.Password);

                    client.SendAsync(message, cancellationToken);
                }
                finally
                {
                    client.Dispose();
                }
            }
        }
    }
}
