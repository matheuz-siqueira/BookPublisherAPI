using BookPublisher.Application.Dtos.Author;

namespace BookPublisher.Application.Interfaces;

public interface IAuthorService
{
    Task<AuthorResponseJson> CreateAsync(RegisterAuthorRequestJson request); 
    Task<AuthorResponseJson> GetByIdAsync(long id);
    Task<IEnumerable<AuthorResponseJson>> GetAllAsync();
    Task RemoveAsync(long id); 
    Task UpdateAsync(long id, UpdateAuthorRequestJson request);
}
