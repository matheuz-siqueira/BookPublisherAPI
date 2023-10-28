using BookPublisher.Application.Dtos.Author;

namespace BookPublisher.Application.Dtos.Book;

public class BookResponseJson
{
    public long Id { get; set; }    
    public string Title { get; set; }   
    public int Edition { get; set; }
}