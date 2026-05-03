using Microsoft.Agents.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newsletter.Ai.Models;
using Newsletter.Ai.Providers.Abstraction;
using Newsletter.Core.Agent.Abstraction;
using Newsletter.Core.Configurations;
using Newsletter.Core.Enums;
using Newsletter.Core.Models;
using OpenAI;
using OpenAI.Chat;
using System.Text.Json;

namespace Newsletter.Ai.Agents;

public sealed class TitleGeneratorAgent(ILogger<TitleGeneratorAgent> logger,
    [FromKeyedServices(PromptProvider.File)] IPromptProvider promptProvider) : IAgent<IEnumerable<Article>, string>
{
    private const string AGENT_NAME = "TitleGeneratorAgent";
    private const string PROMPT = "Gere um título para newsletter semanal com base neste JSON: ";
    private const float TEMPERATURE_EXECUTION_AGENT = 0.7f;

    public async Task<string> RunAsync(IEnumerable<Article> data, CancellationToken cancellationToken)
    {
        logger.LogInformation("Gerando o título da newsletter.");

        var client = new OpenAIClient(apiKey: Configuration.OpenAi.ApiKey);
        var instructions = await promptProvider.GetPromptAsync(AGENT_NAME, cancellationToken);

        var agent = client
            .GetChatClient(AiModels.Gpt4OMini)
            .AsAIAgent(new ChatClientAgentOptions
            {
                Name = AGENT_NAME,
                Description = "Agente especialista em gerar título semanal para newsletter.",
                ChatOptions = new()
                {
                    ModelId = AiModels.Gpt4OMini,
                    Temperature = TEMPERATURE_EXECUTION_AGENT,
                    Instructions = instructions
                }
            });

        var prompt = $"{PROMPT} {JsonSerializer.Serialize(data)}";
        var response = await agent.RunAsync<string>(prompt, cancellationToken: cancellationToken);

        logger.LogInformation("Newsletter gerada.");
        logger.LogInformation("---");
        logger.LogInformation(response.Result);
        logger.LogInformation("---");

        return response.Result;
    }
}