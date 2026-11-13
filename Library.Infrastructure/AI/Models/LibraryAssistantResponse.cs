namespace Library.Application.AI.Models;

public class LibraryAssistantResponse
{
    public string Answer { get; set; } = string.Empty;

    public object? Data { get; set; }

    public string? ActionPerformed { get; set; }
}