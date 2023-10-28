using BookPublisher.Domain.Entities;

namespace BookPublisher.Domain.Interfaces;

public interface IAuthorPerBookRepository
{
    Task CreateAsyn(AuthorPerBook authorPerBook); 
}
