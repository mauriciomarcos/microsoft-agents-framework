using Microsoft.Extensions.Logging;
using Newsletter.Core.Services.Abstraction;

namespace Newsletter.Application.Services;

public sealed class EmailService(ILogger<EmailService> logger) : IEmailService
{
    public async Task SendAsync(string toName, string toEmail, string subject, string body, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);
        logger.LogInformation("Enviando newsletter para {Name} - Título {Subject}", toName, subject);
    }
}