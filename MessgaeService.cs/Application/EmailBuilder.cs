using MessageService.Abstractions;
using System.Net.Mail;

namespace MessageService.Builder
{
    public class EmailBuilder : IEmailBuilder
    {
        private MailMessage message;
        public EmailBuilder() => this.message =  new MailMessage();

        public IEmailBuilder AddTo(string value)
        {
            if (!String.IsNullOrEmpty(value))
                message.To.Add(new MailAddress(value.ToString()!));
            return this;
        }
        public IEmailBuilder AddTo(IEnumerable<string> values)
        {
            if (!values.Any())
                foreach (var x in values) message.To.Add(new MailAddress(x.ToString()!));
            return this;
        }
        public IEmailBuilder AddCC(string value)
        {
            if (!String.IsNullOrEmpty(value))
                message.CC.Add(new MailAddress(value.ToString()!));
            return this;
        }
        public IEmailBuilder AddCC(IEnumerable<string> values)
        {
            if (!values.Any())
                foreach (var x in values) message.CC.Add(new MailAddress(x.ToString()!));
            return this;
        }
        public IEmailBuilder AddBody(string value)
        {
            if (!String.IsNullOrEmpty(value))
                message.Body = value;
            return this;
        }

        public IEmailBuilder AddIsHtml()
        {
            message.IsBodyHtml=true;
            return this;
        }
        public IEmailBuilder AddSubject(string value)
        {
            if (!String.IsNullOrEmpty(value))
                message.Subject = value;
            return this;
        }

        public MailMessage Build() => message;
    }
}
