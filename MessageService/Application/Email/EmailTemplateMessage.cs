using MessageService.Models.Configuration;
using MessageService.Models.Context;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace MessageService.Application.Email
{
    public class EmailTemplateMessage : EmailMessage
    {
        public EmailTemplateMessage(IOptions<EmailConfig> config) : base(config) { }

        public override MailMessage CreateMessage(EmailMessageData data)
        {
            MailMessage emailMessage = new();
            emailMessage.From = new MailAddress(config.From);
            emailMessage.To.Add(data.To.ToString()!);
            emailMessage.Subject = data.Subject;
            emailMessage.IsBodyHtml = true;
            emailMessage.Body = data.Body;

            return emailMessage;
        }
    }
}
