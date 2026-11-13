using Library.Application.AI.Interfaces;
using Library.Application.AI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OllamaTestController : ControllerBase
{
    private readonly IOllamaClient _ollamaClient;

    public OllamaTestController(IOllamaClient ollamaClient)
    {
        _ollamaClient = ollamaClient;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> Generate(
        [FromBody] LibraryQuestionRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Question))
        {
            return BadRequest("Question cannot be empty.");
        }

        var response = await _ollamaClient.GenerateResponseAsync(
            request.Question,
            cancellationToken
        );

        return Ok(new
        {
            question = request.Question,
            answer = response
        });
    }
}