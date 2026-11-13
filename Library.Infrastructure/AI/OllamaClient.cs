
using System.Net.Http.Json;
using Library.Application.AI.Interfaces;

namespace Library.Infrastructure.AI;

public class OllamaClient : IOllamaClient
{
    private readonly HttpClient _httpClient;

    public OllamaClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GenerateResponseAsync(
        string prompt,
        CancellationToken cancellationToken = default)
    {
        var request = new
        {
            model = "qwen2.5:3b",
            prompt = prompt,
            stream = false
        };

        var response = await _httpClient.PostAsJsonAsync(
            "/api/generate",
            request,
            cancellationToken
        );

        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<OllamaResponse>(
                cancellationToken: cancellationToken
            );

        return result?.Response ?? string.Empty;
    }

    private class OllamaResponse
    {
        public string Response { get; set; } = string.Empty;
    }
}
