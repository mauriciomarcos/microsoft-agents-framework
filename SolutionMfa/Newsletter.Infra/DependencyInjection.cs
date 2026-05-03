using Microsoft.Extensions.DependencyInjection;
using Newsletter.Core.Repositories.Abstraction;
using Newsletter.Infra.Repositories;

namespace Newsletter.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddScoped<IArticleRepository, ArticleRepository>()
            .AddScoped<ISubscriberRepository, SubscriberRepository>();
}