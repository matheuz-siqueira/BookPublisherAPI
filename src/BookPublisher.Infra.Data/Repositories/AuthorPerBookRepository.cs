using BookPublisher.Domain.Entities;
using BookPublisher.Domain.Interfaces;
using BookPublisher.Infra.Data.Context;

namespace BookPublisher.Infra.Data.Repositories;

public class AuthorPerBookRepository : IAuthorPerBookRepository
{
    private readonly AppDbContext _context;
    public AuthorPerBookRepository(AppDbContext context)
    {
        _context = context; 
    }
    public async Task CreateAsyn(AuthorPerBook authorPerBook)
    {
        await _context.AuthorPerBooks.AddAsync(authorPerBook); 
        await _context.SaveChangesAsync();
    }
}
