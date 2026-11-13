using Library.Application.AI.Models;

namespace Library.Application.AI.Interfaces;

public interface ILibraryDataQueryService
{
    Task<object?> ExecuteQueryAsync(
        LibraryQueryPlan plan,
        CancellationToken cancellationToken = default
    );
}