
using Library.Core.Entities;

namespace Library.Application.Interfaces;

public interface IBookRepository
{
    Task<List<Book>> GetAllAsync();

    Task<Book?> GetByIdAsync(int id);

    Task AddAsync(Book book);

    Task UpdateAsync(Book book);

    Task DeleteAsync(Book book);
}
