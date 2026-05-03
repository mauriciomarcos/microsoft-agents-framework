namespace Newsletter.Core.Services.Abstraction;

public interface IEmailService
{
    Task SendAsync(string toName, string toEmail, string subject, string body, CancellationToken cancellationToken);
}