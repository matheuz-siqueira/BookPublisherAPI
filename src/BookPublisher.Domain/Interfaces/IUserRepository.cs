using BookPublisher.Domain.Entities;

namespace BookPublisher.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> CreateAsync(User user); 
    Task<User> GetProfileAsync(long id);
    Task<User> GetByEmailAsync(string email);
    Task UpdatePasswordAync();  
}
