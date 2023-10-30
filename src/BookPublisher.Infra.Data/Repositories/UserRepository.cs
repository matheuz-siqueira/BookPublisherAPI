using BookPublisher.Domain.Entities;
using BookPublisher.Domain.Interfaces;
using BookPublisher.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace BookPublisher.Infra.Data.Repositories;

public class UserRepository : IUserRepository
{

    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context; 
    }
    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user); 
        await _context.SaveChangesAsync(); 
        return user; 
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetProfileAsync(long id)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id); 
    }

    public async Task UpdatePasswordAync()
    {
        await _context.SaveChangesAsync();
    }
}
