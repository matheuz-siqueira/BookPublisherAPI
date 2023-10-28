namespace BookPublisher.Application.Exceptions.BookPublisherExceptions;

public class RepeatedIdException : Exception
{
    public RepeatedIdException(string message) : base (message)
    { }
}
