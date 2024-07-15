using MessageService.Abstractions;
using MessageService.Models.Configuration;
using MessageService.Models.Context;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace MessageService.Application.Email
{
    public class EmailMessage : IMessage<EmailMessageData, MailMessage>
    {
        protected readonly EmailConfig config;

        public EmailMessage(IOptions<EmailConfig> config) =>
            this.config = config.Value ?? throw new ArgumentNullException(nameof(config));

        public virtual MailMessage CreateMessage(EmailMessageData data)
        {
            MailMessage emailMessage = new();
            emailMessage.From = new MailAddress(config.From);
            emailMessage.To.Add(data.To.ToString()!);
            emailMessage.Subject = data.Subject;
            emailMessage.Body = data.Body;

            return emailMessage;
        }
    }
}