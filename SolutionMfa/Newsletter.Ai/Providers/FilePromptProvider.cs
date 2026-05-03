using Newsletter.Ai.Providers.Abstraction;

namespace Newsletter.Ai.Providers;

public sealed class FilePromptProvider : IPromptProvider
{
    public async Task<string> GetPromptAsync(string agentName, CancellationToken cancellationToken)
    {
        var assembly = typeof(FilePromptProvider).Assembly;
        var resourceName = $"Newsletter.Ai.Prompts.{agentName}.md";

        await using var stream = assembly.GetManifestResourceStream(resourceName) ??
            throw new FileNotFoundException($"Prompt for {agentName} not found: {resourceName}");

        using var reader = new StreamReader(stream);

        return await reader.ReadToEndAsync(cancellationToken);
    }
}