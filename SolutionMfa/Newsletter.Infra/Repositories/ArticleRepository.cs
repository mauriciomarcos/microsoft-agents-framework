using Newsletter.Core.Models;
using Newsletter.Core.Repositories.Abstraction;

namespace Newsletter.Infra.Repositories;

public sealed class ArticleRepository : IArticleRepository
{
    public async Task<IEnumerable<Article>> GetFromLastWeekAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(150, cancellationToken);

        return
        [
            new Article(
                Title: "Dapper: Mapeando consultas complexas",
                Url: "htpps://caminho.conteudo.com",
                Content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                PublishDate: DateTime.UtcNow.AddDays(-1)
            ),
            new Article(
                Title: "Aprendendo I.A: Microsoft Agents Framework",
                Url: "htpps://caminho.conteudo.com",
                Content: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                PublishDate: DateTime.UtcNow.AddDays(-2)
            )
        ];
    }
}