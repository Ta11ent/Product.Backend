using MessageService.Abstractions;
using MessageService.Application;
using MessageService.Builder;
using MessageService.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace MessageService
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEmailSender(this IServiceCollection services, Action<EmailConfig> configureOptions)
        {
            services.Configure<EmailConfig>(configureOptions);
            services.TryAddSingleton<IEmailSender, EmailSender>();
            return services;
        }

        public static IServiceCollection AddEmailBuilder(this IServiceCollection services)
        {
            services.TryAddScoped<IEmailBuilder, EmailBuilder>();
            services.TryAddScoped<IEmailHandler, EmailHandler>();
            return services;
        }

    }
}
