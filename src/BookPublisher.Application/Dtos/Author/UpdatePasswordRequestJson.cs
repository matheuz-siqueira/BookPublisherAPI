namespace BookPublisher.Application.Dtos.Author;

public class UpdatePasswordRequestJson
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
