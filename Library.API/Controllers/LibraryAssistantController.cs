using Library.Application.AI.Interfaces;
using Library.Application.AI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibraryAssistantController : ControllerBase
{
    private readonly ILibraryAssistantService _assistantService;

    public LibraryAssistantController(
        ILibraryAssistantService assistantService)
    {
        _assistantService = assistantService;
    }

    [HttpPost("ask")]
    public async Task<IActionResult> Ask(
        [FromBody] LibraryQuestionRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _assistantService.AskAsync(
            request,
            cancellationToken);

        return Ok(response);
    }
}