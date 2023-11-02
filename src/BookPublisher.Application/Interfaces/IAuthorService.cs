using BookPublisher.Application.Dtos.Author;
using BookPublisher.Domain.Entities;
using BookPublisher.Domain.Pagination;

namespace BookPublisher.Application.Interfaces;

public interface IAuthorService
{
    Task<AuthorResponseJson> CreateAsync(RegisterAuthorRequestJson request); 
    Task<AuthorResponseJson> GetByIdAsync(long id);
    PagedList<Author> GetAllAsync(AuthorParameters authorParameters);
    Task RemoveAsync(long id); 
    Task UpdateAsync(long id, UpdateAuthorRequestJson request);
}
