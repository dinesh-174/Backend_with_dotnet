using Library.Application.AI.Models;

namespace Library.Application.AI.Interfaces;

public interface ILibraryQueryPlanner
{
    Task<LibraryQueryPlan> CreatePlanAsync(
        string question,
        CancellationToken cancellationToken = default
    );
}