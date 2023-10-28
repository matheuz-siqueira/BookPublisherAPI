using BookPublisher.Domain.Entities;

namespace BookPublisher.Domain.Interfaces;

public interface IBookRepository
{
    Task<Book> CreateAsync(Book book); 
    Task<Book> GetByIdAsync(long id); 
    Task<Book> GetByIdTracking(long id);
    Task<IEnumerable<Book>> GetAllAsync();
    Task RemoveAsync(Book book); 
    Task Update();   
}
