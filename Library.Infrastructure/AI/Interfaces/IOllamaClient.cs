namespace Library.Application.AI.Interfaces;

public interface IOllamaClient
{
    Task<string> GenerateResponseAsync(
        string prompt,
        CancellationToken cancellationToken = default
    );
}