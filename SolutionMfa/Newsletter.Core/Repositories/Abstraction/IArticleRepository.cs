using Newsletter.Core.Models;

namespace Newsletter.Core.Repositories.Abstraction;

public interface IArticleRepository
{
    Task<IEnumerable<Article>> GetFromLastWeekAsync(CancellationToken cancellationToken);
}