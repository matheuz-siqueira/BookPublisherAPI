using BookPublisher.Domain.Entities;
using BookPublisher.Domain.Interfaces;
using BookPublisher.Domain.Pagination;
using BookPublisher.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BookPublisher.Infra.Data.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context; 
    public BookRepository(AppDbContext context)
    {
        _context = context; 
    }
    public async Task<Book> CreateAsync(Book book)
    {
        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync(); 
        return book; 
    }

    public PagedList<Book> GetAllAsync(BookParameters bookParameters)
    {
        var items = _context.Set<Book>().AsNoTracking();
        return PagedList<Book>.ToPagedList(items.OrderBy(b => b.Title),
            bookParameters.PageNumber, bookParameters.PageSize);
    }

    public Task<Book> GetByIdAsync(long id)
    {
        return _context.Books.AsNoTracking()
            .Include(b => b.AuthorPerBooks)
            .FirstOrDefaultAsync(b => b.Id == id); 
    }

    public Task<Book> GetByIdTracking(long id)
    {
        return _context.Books.FirstOrDefaultAsync(b => b.Id == id); 
    }

    public async Task RemoveAsync(Book book)
    {
        _context.Remove(book); 
        await _context.SaveChangesAsync(); 
    }

    public async Task Update()
    {
        await _context.SaveChangesAsync();
    }
}
