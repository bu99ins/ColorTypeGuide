using System.Text.Json;
using Microsoft.Extensions.AI;

namespace ColorTypeGuide.AiClient;

public class SimpleCompletion(IChatClient chatClient, ChatOptions? options = null) : ICompletionStrategy
{
    public async Task<(T?, ChatFinishReason?)> CompleteAsync<T>(List<ChatMessage> chatMessages, CancellationToken token)
    {
        var result = await chatClient.GetResponseAsync<T>(chatMessages, options, cancellationToken: token);

        if (result.Result is not null)
        {
            Console.WriteLine(JsonSerializer.Serialize(result.Result, new JsonSerializerOptions { WriteIndented = true }));
        }
        else
        {
            Console.Write(result.Text);
        }

        return (result.Result, result.FinishReason);
    }
}
