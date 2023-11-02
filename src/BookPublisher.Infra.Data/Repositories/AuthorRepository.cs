using BookPublisher.Domain.Entities;
using BookPublisher.Domain.Interfaces;
using BookPublisher.Domain.Pagination;
using BookPublisher.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BookPublisher.Infra.Data.Repositories;

public class AuthorRepository : IAuthorRepository
{

    private readonly AppDbContext _context;
    public AuthorRepository(AppDbContext context)
    {
        _context = context; 
    }

    public async Task<Author> CreateAsync(Author author)
    {
        await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public async Task DeleteAsync(Author author)
    {
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }

    public PagedList<Author> GetAllAsync(AuthorParameters authorParameters)
    {
        var items = _context.Set<Author>().AsNoTracking(); 
        return PagedList<Author>.ToPagedList(items.OrderBy(a => a.FirstName), 
            authorParameters.PageNumber, authorParameters.PageSize);
    }

    public Task<Author> GetByIdAsync(long id)
    {
        return _context.Authors.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id); 
    }

    public Task<Author> GetByIdTracking(long id)
    {
       return _context.Authors.FirstOrDefaultAsync(a => a.Id == id); 
    }

    public async Task UpdateAsync()
    {
        await _context.SaveChangesAsync(); 
    }
}
