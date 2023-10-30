namespace BookPublisher.Application.Exceptions.BookPublisherExceptions;

public class ExistingUserException : Exception
{
    public ExistingUserException(string message) : base (message)
    { }
}
