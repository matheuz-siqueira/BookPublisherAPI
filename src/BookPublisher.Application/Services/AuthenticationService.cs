using System.Text;
using AutoMapper; 
using Microsoft.IdentityModel.Tokens;
using BookPublisher.Application.Dtos.User;
using BookPublisher.Application.Exceptions.BookPublisherExceptions;
using BookPublisher.Application.Interfaces;
using BookPublisher.Domain.Entities;
using BookPublisher.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace BookPublisher.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _repository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    public AuthenticationService(IUserRepository repository, IConfiguration configuration, 
        IMapper mapper)
    {
        _repository = repository; 
        _configuration = configuration;
        _mapper = mapper; 
    }
    public async Task<GetTokenResponseJson> Login(AuthenticationRequestJson request)
    {
        var user = await _repository.GetByEmailAsync(request.Email);
        if((user is null) || (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password)))
        {
            throw new IncorretCredentialsException("incorret email or password"); 
        } 

        (string token, DateTime expiration) = GenerateToken(user);
        var response = new GetTokenResponseJson
        {
            UserName = user.UserName,
            Token = token, 
            Expiration = expiration 
        };
        return response; 
    }

    private (string, DateTime) GenerateToken(User user)
    {
        var JWTKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var expiration = DateTime.Now.AddHours(4);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(JWTKey),
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, user.FullName));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())); 
        claims.Add(new Claim("UserName", user.UserName)); 

        var tokenJWT = new JwtSecurityToken(
            expires : expiration, 
            signingCredentials: credentials, 
            claims: claims
        );  

        var token = new JwtSecurityTokenHandler().WriteToken(tokenJWT); 
        return (token, expiration); 
    }
}
