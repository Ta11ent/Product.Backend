using System.Net.Mail;

namespace MessageService.Abstractions
{
    public interface IEmailBuilder
    {
        IEmailBuilder AddTo(string value);
        IEmailBuilder AddTo(IEnumerable<string> values);
        IEmailBuilder AddCC(string value);
        IEmailBuilder AddCC(IEnumerable<string> values);
        IEmailBuilder AddSubject(string value);
        IEmailBuilder AddBody(string value);
        IEmailBuilder AddIsHtml();
        MailMessage Build();
    }
}
