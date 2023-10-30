using AutoMapper;
using BookPublisher.Application.Dtos.User;
using BookPublisher.Application.Exceptions.BookPublisherExceptions;
using BookPublisher.Application.Interfaces;
using BookPublisher.Domain.Entities;
using BookPublisher.Domain.Interfaces;

namespace BookPublisher.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authentication;
    public UserService(IUserRepository repository, IMapper mapper, 
        IAuthenticationService authentication)
    {
        _repository = repository;
        _mapper = mapper;  
        _authentication = authentication; 
    }
    public async Task<GetTokenResponseJson> CreateAsync(RegisterUserRequestJon request)
    {
        var existing = await _repository.GetByEmailAsync(request.Email); 
        if(existing is not null)
        {
            throw new ExistingUserException("user already exists");    
        }
        var login = _mapper.Map<AuthenticationRequestJson>(request);
        request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password); 
        var user = _mapper.Map<User>(request);
        await _repository.CreateAsync(user); 
        return await _authentication.Login(login); 
    }
}
