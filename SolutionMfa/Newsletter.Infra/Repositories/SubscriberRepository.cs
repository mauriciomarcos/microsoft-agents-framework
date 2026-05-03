using Newsletter.Core.Models;
using Newsletter.Core.Repositories.Abstraction;

namespace Newsletter.Infra.Repositories;

public sealed class SubscriberRepository : ISubscriberRepository
{
    public async Task<IEnumerable<Subscriber>> GetAllAsync(CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);

        return
        [
            new Subscriber(Name: "Maurício Marcos", "mmarcos@gmail.com"),
            new Subscriber(Name: "Juliana de Campos Coelho Marcos", "juliana@gmail.com"),
            new Subscriber(Name: "Anthony de Campos Marcos", "anthony@gmail.com")
        ];
    }
}