namespace Newsletter.Ai.Providers.Abstraction;

public interface IPromptProvider
{
    Task<string> GetPromptAsync(string agentName, CancellationToken cancellationToken);
}