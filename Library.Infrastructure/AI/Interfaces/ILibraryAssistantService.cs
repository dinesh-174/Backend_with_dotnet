using Library.Application.AI.Models;

namespace Library.Application.AI.Interfaces;

public interface ILibraryAssistantService
{
    Task<LibraryAssistantResponse> AskAsync(
        LibraryQuestionRequest request,
        CancellationToken cancellationToken = default
    );
}