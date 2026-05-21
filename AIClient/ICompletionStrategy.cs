using Microsoft.Extensions.AI;

namespace ColorTypeGuide.AiClient;

public interface ICompletionStrategy
{
    Task<(string, ChatFinishReason?)> CompleteAsync(List<ChatMessage> chatMessages, CancellationToken token);
}