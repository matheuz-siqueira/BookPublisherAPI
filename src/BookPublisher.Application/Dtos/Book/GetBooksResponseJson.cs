namespace BookPublisher.Application.Dtos.Book;

public class GetBooksResponseJson
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }    
    public int Edition { get; set; }
    public DateOnly LaunchDate { get; set; }
}
