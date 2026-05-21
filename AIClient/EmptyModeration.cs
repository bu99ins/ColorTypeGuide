using Microsoft.Extensions.AI;

namespace ColorTypeGuide.AiClient;

public class EmptyModeration() : IModeration
{
    public Task<ChatMessage> GetModeratedInputAsync(string? s)
    {
        return Task.FromResult(new ChatMessage(ChatRole.User, s));
    }
}