using MessageService.Builder;
using MessageService.Abstractions;
using MessageService.Models;
using System.Net.Mail;

namespace MessageService.Application
{
    public class EmailHandler : IEmailHandler
    {
        public MailMessage CreateHtmlEmail(EmailMessageData data)
            => new EmailBuilder()
            .AddTo(data.To)
            .AddCC(data.CC!)
            .AddSubject(data.Subject)
            .AddBody(data.Body)
            .AddIsHtml()
            .Build();

        public MailMessage CreateTextEmail(EmailMessageData data)
         => new EmailBuilder()
            .AddTo(data.To)
            .AddCC(data.CC!)
            .AddSubject(data.Subject)
            .AddBody(data.Body)
            .Build();
    }
}
