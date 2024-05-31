using MessageService.Abstractions;
using MessageService.Application;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;

namespace MessageService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSenderService(this IServiceCollection service)
        {
            service.AddTransient<ISender, Sender>();
            service.AddTransient<IMessage<MailMessage>, EmailMessage>();

            return service;
        }
    }
}
