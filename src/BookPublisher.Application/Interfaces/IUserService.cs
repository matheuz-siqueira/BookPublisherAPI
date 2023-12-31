using BookPublisher.Application.Dtos.Author;
using BookPublisher.Application.Dtos.User;

namespace BookPublisher.Application.Interfaces;

public interface IUserService
{
    Task<GetTokenResponseJson> CreateAsync(RegisterUserRequestJon request);   
    Task<GetProfileResponseJson> GetProfileAsync();
    Task UpdatePasswordAsync(UpdatePasswordRequestJson request);
}
