using MessageService.Abstractions;
using MessageService.Application.Email;
using MessageService.Models.Context;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;

namespace MessageService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSenderEmail(this IServiceCollection service)
        {
            service.AddTransient<ISender<EmailMessageData>, SendEmail>();
            service.AddTransient<IMessage<EmailMessageData, MailMessage>, EmailMessage>();

            return service;
        }

        public static IServiceCollection AddSenderEmailTamplate(this IServiceCollection service)
        {
            service.AddTransient<ISender<EmailMessageData>, SendEmail>();
            service.AddTransient<IMessage<EmailMessageData, MailMessage>, EmailTemplateMessage>();

            return service;
        }
    }
}
