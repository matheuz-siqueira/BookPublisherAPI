using BookPublisher.Application.Dtos.Book;
using BookPublisher.Domain.Pagination;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookPublisher.Application.Interfaces;

public interface IBookService
{
    Task<BookResponseJson> CreateAsync(RegisterBookRequestJson request); 
    Task<GetBookResponseJson> GetByIdAsync(long id); 
    PagedList<Domain.Entities.Book> GetAllAsync(BookParameters bookParameters);
    Task UpdateAsync(UpdateBookRequestJson request, long id); 
    Task DeleteAsync(long id);   
}
