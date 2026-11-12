
using Library.Application.Interfaces;
using Library.Core.Entities;

namespace Library.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _bookRepository.GetByIdAsync(id);
    }

    public async Task AddBookAsync(Book book)
    {
        await _bookRepository.AddAsync(book);
    }

    public async Task UpdateBookAsync(Book book)
    {
        await _bookRepository.UpdateAsync(book);
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book == null)
        {
            throw new KeyNotFoundException($"Book with ID {id} not found.");
        }

        await _bookRepository.DeleteAsync(book);
    }
}
