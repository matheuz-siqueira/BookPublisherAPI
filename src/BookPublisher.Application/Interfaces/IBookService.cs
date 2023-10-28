using BookPublisher.Application.Dtos.Book;

namespace BookPublisher.Application.Interfaces;

public interface IBookService
{
    Task<BookResponseJson> CreateAsync(RegisterBookRequestJson request); 
    Task<GetBookResponseJson> GetByIdAsync(long id); 
    Task<IEnumerable<GetBooksResponseJson>> GetAllAsync();
    Task UpdateAsync(UpdateBookRequestJson request, long id); 
    Task DeleteAsync(long id);   
}
