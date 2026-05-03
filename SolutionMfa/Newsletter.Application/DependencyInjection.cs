using Microsoft.Extensions.DependencyInjection;
using Newsletter.Application.Services;
using Newsletter.Core.Services.Abstraction;

namespace Newsletter.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddServicesApplication(this IServiceCollection services) =>
        services
            .AddScoped<INewsletterService, NewsletterService>()
            .AddScoped<IEmailService, EmailService>();
}