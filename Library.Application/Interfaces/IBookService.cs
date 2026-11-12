
using Library.Core.Entities;

namespace Library.Application.Interfaces;

public interface IBookService
{
    Task<List<Book>> GetAllBooksAsync();

    Task<Book?> GetBookByIdAsync(int id);

    Task AddBookAsync(Book book);

    Task UpdateBookAsync(Book book);

    Task DeleteBookAsync(int id);
}
