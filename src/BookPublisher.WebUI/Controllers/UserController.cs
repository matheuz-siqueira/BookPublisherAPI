using BookPublisher.Application.Dtos.User;
using BookPublisher.Application.Exceptions.ValidatorsExceptions;
using BookPublisher.Application.Interfaces;
using BookPublisher.Domain.DomainValidation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookPublisher.WebUI.Controllers;

[Route("api/v{version:apiVersion}/users")]
public class UserController : BookPublisherController
{
    private readonly IUserService _service;
    private readonly IValidator<RegisterUserRequestJon> _validatorRegisterUser;
    public UserController(IUserService service, 
        IValidator<RegisterUserRequestJon> validatorRegisterUser)
    {
        _service = service;
        _validatorRegisterUser = validatorRegisterUser; 
    }

    [HttpPost]
    public async Task<ActionResult> PostUser(RegisterUserRequestJon request)
    {

        var result = _validatorRegisterUser.Validate(request);
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        try
        {
            var response = await _service.CreateAsync(request); 
            return StatusCode(201, response);
        }
        catch(DomainExceptionValidation e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}
