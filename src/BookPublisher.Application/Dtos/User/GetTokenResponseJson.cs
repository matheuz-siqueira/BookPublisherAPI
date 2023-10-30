namespace BookPublisher.Application.Dtos.User;

public class GetTokenResponseJson
{
    public string UserName { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}
