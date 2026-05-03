using Newsletter.Core.Models;

namespace Newsletter.Core.Repositories.Abstraction;

public interface ISubscriberRepository
{
    Task<IEnumerable<Subscriber>> GetAllAsync(CancellationToken cancellationToken);
}