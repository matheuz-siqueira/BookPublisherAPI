namespace BookPublisher.Application.Exceptions.BookPublisherExceptions;

public class IncorretCredentialsException : Exception
{
    public IncorretCredentialsException(string message) : base (message)
    { }
}
