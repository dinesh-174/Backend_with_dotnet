
using Library.Application.AI.Interfaces;
using Library.Application.AI.Models;
using Library.Core.Entities;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.AI;

public class LibraryDataQueryService : ILibraryDataQueryService
{
    private readonly AppDbContext _context;

    public LibraryDataQueryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<object?> ExecuteQueryAsync(
        LibraryQueryPlan plan,
        CancellationToken cancellationToken = default)
    {
        return plan.Action switch
        {
            "CountBooks" =>
                await _context.Books.CountAsync(cancellationToken),

            "CountAvailableBooks" =>
                await _context.Books.CountAsync(
                    b => b.IsAvailable,
                    cancellationToken),

            "CountUnavailableBooks" =>
                await _context.Books.CountAsync(
                    b => !b.IsAvailable,
                    cancellationToken),

            "SearchBooks" =>
                await SearchBooksAsync(plan, cancellationToken),

            "GetBooksByCategory" =>
                await GetBooksByCategoryAsync(plan, cancellationToken),

            "CountMembers" =>
                await _context.Members.CountAsync(cancellationToken),

            "CountActiveMembers" =>
                await _context.Members.CountAsync(
                    m => m.IsActive,
                    cancellationToken),

            "CountInactiveMembers" =>
                await _context.Members.CountAsync(
                    m => !m.IsActive,
                    cancellationToken),

            "GetLibraryOverview" =>
                await GetLibraryOverviewAsync(cancellationToken),

            _ => throw new NotSupportedException(
                $"The requested action '{plan.Action}' is not supported.")
        };
    }

    private async Task<List<Book>> SearchBooksAsync(
        LibraryQueryPlan plan,
        CancellationToken cancellationToken)
    {
        var query = _context.Books.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(plan.SearchTerm))
        {
            var searchTerm = plan.SearchTerm.Trim();

            query = query.Where(b =>
                b.Title.Contains(searchTerm) ||
                b.ISBN.Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(plan.Category))
        {
            var category = plan.Category.Trim();

            query = query.Where(b => b.Category == category);
        }

        if (plan.PublishedAfter.HasValue)
        {
            query = query.Where(
                b => b.PublishedYear > plan.PublishedAfter.Value);
        }

        return await query
            .OrderBy(b => b.Title)
            .Take(50)
            .ToListAsync(cancellationToken);
    }

    private async Task<List<Book>> GetBooksByCategoryAsync(
        LibraryQueryPlan plan,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(plan.Category))
        {
            throw new ArgumentException(
                "A category is required for this operation.");
        }

        var category = plan.Category.Trim();

        return await _context.Books
            .AsNoTracking()
            .Where(b => b.Category == category)
            .OrderBy(b => b.Title)
            .Take(50)
            .ToListAsync(cancellationToken);
    }

    private async Task<object> GetLibraryOverviewAsync(
        CancellationToken cancellationToken)
    {
        var totalBooks = await _context.Books.CountAsync(cancellationToken);

        var availableBooks = await _context.Books.CountAsync(
            b => b.IsAvailable,
            cancellationToken);

        var unavailableBooks = totalBooks - availableBooks;

        var totalMembers = await _context.Members.CountAsync(
            cancellationToken);

        var activeMembers = await _context.Members.CountAsync(
            m => m.IsActive,
            cancellationToken);

        return new
        {
            TotalBooks = totalBooks,
            AvailableBooks = availableBooks,
            UnavailableBooks = unavailableBooks,
            TotalMembers = totalMembers,
            ActiveMembers = activeMembers,
            InactiveMembers = totalMembers - activeMembers
        };
    }
}
