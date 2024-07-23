using MessageService.Abstractions;
using MessageService.Models;
using System.Net.Mail;

namespace MessageService.Abstractions
{
    public interface IEmailHandler
    {
        MailMessage CreateTextEmail(EmailMessageData data);
        MailMessage CreateHtmlEmail(EmailMessageData data);
    }
}
