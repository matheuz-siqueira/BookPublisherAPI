namespace BookPublisher.Application.Dtos.Book;

public class RegisterBookRequestJson
{
    public string Title { get; set; }
    public string SubTitle { get; set; }    
    public int Edition { get; set; }
    public DateOnly LaunchDate { get; set; }    
    public int Quantity { get; set; }   
    public decimal Price { get; set; }
    public List<RegisterAuthorIdRequestJson> AuthorsId { get; set; } = new();

}
