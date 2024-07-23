using MessageService.Abstractions;
using System.Net.Mail;

namespace MessageService.Abstractions
{
    public interface IEmailSender : IMessageSender<MailMessage>
    { }
}
