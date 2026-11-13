
namespace Library.Application.AI.Models;

public class LibraryQueryPlan
{
    public string Action { get; set; } = string.Empty;

    public string? SearchTerm { get; set; }

    public string? Category { get; set; }

    public int? PublishedAfter { get; set; }
}
