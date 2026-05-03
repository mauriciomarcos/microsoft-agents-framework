using Newsletter.Ai.Agents;
using Newsletter.Ai.Providers;
using Newsletter.Ai.Providers.Abstraction;
using Newsletter.Core.Agent.Abstraction;
using Newsletter.Core.Enums;
using Newsletter.Core.Models;

namespace Newsletter.Api.Configs.DependencyInjections;

public static class DependencyInjection
{
    public static IServiceCollection AddAgents(this IServiceCollection services)
    {
        services
            .AddKeyedTransient<IAgent<IEnumerable<Article>, string>, TitleGeneratorAgent>(AgentType.TitleGenerator);

        services
            .AddKeyedTransient<IAgent<IEnumerable<Article>, string>, NewsletterGeneratorAgent>(AgentType.NewsletterGenerator);

        services
            .AddKeyedTransient<IPromptProvider, FilePromptProvider>(PromptProvider.File);

        return services;
    }
}