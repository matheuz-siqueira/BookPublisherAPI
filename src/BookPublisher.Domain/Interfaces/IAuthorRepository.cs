using BookPublisher.Domain.Entities;

namespace BookPublisher.Domain.Interfaces;

public interface IAuthorRepository
{
    Task<Author> CreateAsync(Author author); 
    Task<Author> GetByIdAsync(long id); 
    Task<Author> GetByIdTracking(long id);
    Task<IEnumerable<Author>> GetAllAsync(); 
    Task DeleteAsync(Author author); 
    Task UpdateAsync();
}
