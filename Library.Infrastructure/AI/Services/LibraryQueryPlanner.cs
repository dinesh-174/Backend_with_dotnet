using System.Text.Json;
using Library.Application.AI.Interfaces;
using Library.Application.AI.Models;

namespace Library.Application.AI.Services;

public class LibraryQueryPlanner : ILibraryQueryPlanner
{
    private readonly IOllamaClient _ollamaClient;

    public LibraryQueryPlanner(IOllamaClient ollamaClient)
    {
        _ollamaClient = ollamaClient;
    }

    public async Task<LibraryQueryPlan> CreatePlanAsync(
        string question,
        CancellationToken cancellationToken = default)
    {
        var prompt = $$"""
You are a query planner for a Library Management System.

Convert the librarian's question into a JSON query plan.

Allowed actions:
- CountBooks
- CountAvailableBooks
- CountUnavailableBooks
- SearchBooks
- GetBooksByCategory
- CountMembers
- CountActiveMembers
- CountInactiveMembers
- GetLibraryOverview

Available Book fields:
- Title
- ISBN
- Category
- PublishedYear
- IsAvailable

Available Member fields:
- FullName
- Email
- PhoneNumber
- IsActive

Rules:
1. Return only valid JSON.
2. Never generate SQL queries.
3. Use only the allowed actions.
4. Use null for parameters that are not required.

Expected JSON structure:

{
    "action": "ActionName",
    "searchTerm": null,
    "category": null,
    "publishedAfter": null
}

Librarian question:
{{question}}
""";
        var response = await _ollamaClient.GenerateResponseAsync(
            prompt,
            cancellationToken
        );

        var json = ExtractJson(response);

        var plan = JsonSerializer.Deserialize<LibraryQueryPlan>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );

        return plan ?? new LibraryQueryPlan
        {
            Action = "Unsupported"
        };
    }

    private static string ExtractJson(string response)
    {
        var start = response.IndexOf('{');
        var end = response.LastIndexOf('}');

        if (start < 0 || end < start)
        {
            throw new InvalidOperationException(
                "AI did not return valid JSON."
            );
        }

        return response.Substring(start, end - start + 1);
    }
}