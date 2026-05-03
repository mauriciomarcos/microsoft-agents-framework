namespace Newsletter.Core.Services.Abstraction;

public interface INewsletterService
{
    Task SendAsync(CancellationToken cancellationToken);
}