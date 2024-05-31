using MessageService.Abstractions;
using MessageService.Models.Configuration;
using MessageService.Models.Context;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Text;

namespace MessageService.Application
{
    internal class EmailMessage : IMessage<MailMessage>
    {
        private readonly EmailConfig _config;
        private readonly string _emailtemplatePath;
        public EmailMessage(IOptions<EmailConfig> config)
        {
            _config = config.Value ?? throw new ArgumentNullException(nameof(config));
            _emailtemplatePath = @"..\MessageService\Templates\email.html";
        }
        public MailMessage CreateMessage(OrderDetailsDto data)
        {
            StringBuilder mailText = new();
            using (var str = new StreamReader(_emailtemplatePath))
            {
                mailText.Append(str.ReadToEnd());
                str.Close();
            }

            mailText = mailText
                .Replace("[User]", data.User.UserName)
                .Replace("[Items]", string.Join("", data.ProductRanges.Select(x => $"<li>{x.Name}</li>")))
                .Replace("[Price]", data.Price.ToString());

            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress(_config.From);
            emailMessage.To.Add(data.User.Email);
            emailMessage.Subject = new StringBuilder().Append($"Hi {data.User.UserName}! This is your order list").ToString();
            emailMessage.IsBodyHtml = true;
            emailMessage.Body = mailText.ToString();

            return emailMessage;
        }
    }
}
