using Microsoft.Extensions.AI;

namespace ColorTypeGuide.AiClient;

public interface ICompletionStrategy
{
    Task<(T?, ChatFinishReason?)> CompleteAsync<T>(List<ChatMessage> chatMessages, CancellationToken token);
}
