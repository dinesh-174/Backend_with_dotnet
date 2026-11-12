
using Library.Application.Interfaces;
using Library.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);

        if (book == null)
        {
            return NotFound("Book not found.");
        }

        return Ok(book);
    }
    [HttpPost]
public async Task<IActionResult> AddBook(Book book)
{
    await _bookService.AddBookAsync(book);

    return CreatedAtAction(
        nameof(GetBookById),
        new { id = book.Id },
        book
    );
}
[HttpPut("{id}")]
public async Task<IActionResult> UpdateBook(int id, Book book)
{
    if (id != book.Id)
    {
        return BadRequest("Book ID mismatch.");
    }

    var existingBook = await _bookService.GetBookByIdAsync(id);

    if (existingBook == null)
    {
        return NotFound($"Book with ID {id} not found.");
    }

    existingBook.Title = book.Title;
    existingBook.ISBN = book.ISBN;
    existingBook.PublishedYear = book.PublishedYear;
    existingBook.IsAvailable = book.IsAvailable;

    await _bookService.UpdateBookAsync(existingBook);

    return NoContent();
}
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteBook(int id)
{
    var existingBook = await _bookService.GetBookByIdAsync(id);

    if (existingBook == null)
    {
        return NotFound($"Book with ID {id} not found.");
    }

    await _bookService.DeleteBookAsync(id);

    return NoContent();
}
}
