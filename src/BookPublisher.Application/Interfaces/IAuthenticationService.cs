using BookPublisher.Application.Dtos.User;

namespace BookPublisher.Application.Interfaces;

public interface IAuthenticationService
{
    Task<GetTokenResponseJson> Login(AuthenticationRequestJson request);
}
