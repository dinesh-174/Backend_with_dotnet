
namespace Library.Core.Entities;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public int PublishedYear { get; set; }

    public bool IsAvailable { get; set; } = true;

    public string Category { get; set; } = string.Empty;
}
