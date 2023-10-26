namespace BookPublisher.Application.Exceptions.BookPublisherExceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base (message)
    { }
}
