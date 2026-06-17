using System.Text;
using System.Text.Json;
using Microsoft.Extensions.AI;

namespace ColorTypeGuide.AiClient;

public class StreamingCompletion(IChatClient chatClient, ChatOptions? options = null) : ICompletionStrategy
{
    public async Task<(T?, ChatFinishReason?)> CompleteAsync<T>(List<ChatMessage> chatMessages, CancellationToken token)
    {
        var answerBuilder = new StringBuilder();
        ChatFinishReason? lastReason = null;

        await foreach (var update in chatClient.GetStreamingResponseAsync(chatMessages, options, cancellationToken: token))
        {
            lastReason = update.FinishReason;
            if (update.Contents.Count == 0) continue;

            Console.Write(update.Text);
            answerBuilder.Append(update.Text);
        }

        T? result = default;
        if (answerBuilder.Length > 0)
        {
            try
            {
                result = JsonSerializer.Deserialize<T>(answerBuilder.ToString());
            }
            catch (JsonException)
            {
            }
        }

        return (result, lastReason);
    }
}
