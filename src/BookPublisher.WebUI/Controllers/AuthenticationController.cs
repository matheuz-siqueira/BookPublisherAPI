using BookPublisher.Application.Dtos.User;
using BookPublisher.Application.Exceptions.BookPublisherExceptions;
using BookPublisher.Application.Exceptions.ValidatorsExceptions;
using BookPublisher.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookPublisher.WebUI.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
public class AuthenticationController : BookPublisherController
{
    private readonly IAuthenticationService _service;
    private readonly IValidator<AuthenticationRequestJson> _validatorAuthentication;
    public AuthenticationController(IAuthenticationService service, 
        IValidator<AuthenticationRequestJson> validatorAuthentication)
    {
        _service = service; 
        _validatorAuthentication = validatorAuthentication; 
    } 

    [HttpPost]
    public async Task<ActionResult> Login(AuthenticationRequestJson request)
    {
        var result = _validatorAuthentication.Validate(request); 
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        try 
        {
            var response = await _service.Login(request); 
            return Ok(response);
        }
        catch(IncorretCredentialsException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch
        {
            return BadRequest();
        }
    }
}
