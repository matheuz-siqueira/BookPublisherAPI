namespace BookPublisher.Application.Exceptions.BookPublisherExceptions;

public class AuthorRequiredException : Exception
{
    public AuthorRequiredException(string message) : base (message)
    { }
}
