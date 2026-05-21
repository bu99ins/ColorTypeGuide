using Microsoft.Extensions.AI;

namespace ColorTypeGuide.AiClient;

public interface IModeration
{
    Task<ChatMessage> GetModeratedInputAsync(string? s);
}