namespace BookPublisher.Application.Exceptions.BookPublisherExceptions;

public class IncorretPasswordException : Exception
{
    public IncorretPasswordException(string message) : base (message)
    { }
}
