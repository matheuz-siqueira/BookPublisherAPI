using BookPublisher.Domain.Entities;
using BookPublisher.Domain.Pagination;

namespace BookPublisher.Domain.Interfaces;

public interface IBookRepository
{
    Task<Book> CreateAsync(Book book); 
    Task<Book> GetByIdAsync(long id); 
    Task<Book> GetByIdTracking(long id);
    PagedList<Book> GetAllAsync(BookParameters bookParameters);
    Task RemoveAsync(Book book); 
    Task Update();   
}
