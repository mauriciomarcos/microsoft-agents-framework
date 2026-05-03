using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newsletter.Core.Agent.Abstraction;
using Newsletter.Core.Enums;
using Newsletter.Core.Models;
using Newsletter.Core.Repositories.Abstraction;
using Newsletter.Core.Services.Abstraction;

namespace Newsletter.Application.Services;

public sealed class NewsletterService(ILogger<NewsletterService> logger,
    [FromKeyedServices(AgentType.TitleGenerator)] IAgent<IEnumerable<Article>, string> titleGeneratorAgent,
    [FromKeyedServices(AgentType.NewsletterGenerator)] IAgent<IEnumerable<Article>, string> newsletterGeneratorAgent,
    IArticleRepository articleRepository,
    ISubscriberRepository subscriberRepository,
    IEmailService emailService) : INewsletterService
{
    public async Task SendAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Recuperando os posts da semana.");

        var posts = await articleRepository.GetFromLastWeekAsync(cancellationToken);
        if (!posts.Any())
            return;

        logger.LogInformation("Gerando título da Newsletter com o agente.");
        var subject = await titleGeneratorAgent.RunAsync(posts, cancellationToken);

        logger.LogInformation("Gerando o conteúdo da Newsletter com o agente.");
        var newsletter = await newsletterGeneratorAgent.RunAsync(posts, cancellationToken);

        logger.LogInformation("Buscando os inscritos na Newsletter.");
        var subscribers = await subscriberRepository.GetAllAsync(cancellationToken);

        logger.LogInformation("Enviando email para os inscritos na Newsletter.");
        foreach (var subscriber in subscribers)
            await emailService.SendAsync(subscriber.Name, subscriber.Email, subject, newsletter, cancellationToken);
    }
}