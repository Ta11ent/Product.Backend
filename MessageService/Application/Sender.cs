using MessageService.Abstractions;
using MessageService.Models.Configuration;
using MessageService.Models.Context;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;

namespace MessageService.Application
{
    public class Sender: ISender
    {
        private readonly EmailConfig _config;
        private readonly IMessage<MailMessage> _message;
        public Sender(IOptions<EmailConfig> config, IMessage<MailMessage> messgae) {
            _config = config.Value ?? throw new ArgumentNullException(nameof(config));
            _message = messgae;
        }
        public void SendMessage(string obj)
        {
            var data = JsonConvert.DeserializeObject<OrderDetailsDto>(obj);
            var message = _message.CreateMessage(data);
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
