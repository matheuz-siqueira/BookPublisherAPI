using BookPublisher.Domain.DomainValidation;

namespace BookPublisher.Domain.Entities;

public sealed class AuthorPerBook : BaseEntity
{
    public long AuthorId { get; set; }
    public long BookId { get; set; }    
    public Book Book { get; set; }
    public Author Author { get; set; }

    public AuthorPerBook(long authorId, long bookId)
    {
        DomainExceptionValidation.When(authorId < 1, "Invalid id value"); 
        DomainExceptionValidation.When(bookId < 1, "Invalid id value");   
        AuthorId = authorId; 
        BookId = bookId;  
    }

}
