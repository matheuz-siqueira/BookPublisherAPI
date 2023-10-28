using BookPublisher.Application.Dtos.Book;

namespace BookPublisher.Application.Interfaces;

public interface IBookService
{
    Task<BookResponseJson> CreateAsync(RegisterBookRequestJson request); 
    // Task<BookResponseJson> GetByIdAsync(long id); 
    // Task<IEnumerable<BookResponseJson>> GetAllAsync();
    // Task UpdateAsync(UpdateBookRequestJson request, long id); 
    // Task Delete(long id);   
}
