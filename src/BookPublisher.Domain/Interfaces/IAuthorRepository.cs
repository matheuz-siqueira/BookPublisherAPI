using BookPublisher.Domain.Entities;
using BookPublisher.Domain.Pagination;

namespace BookPublisher.Domain.Interfaces;

public interface IAuthorRepository
{
    Task<Author> CreateAsync(Author author); 
    Task<Author> GetByIdAsync(long id); 
    Task<Author> GetByIdTracking(long id);
    PagedList<Author> GetAllAsync(AuthorParameters author); 
    Task DeleteAsync(Author author); 
    Task UpdateAsync();
}
