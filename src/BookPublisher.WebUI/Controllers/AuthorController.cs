using System.Text.Json;
using AutoMapper;
using BookPublisher.Application.Dtos.Author;
using BookPublisher.Application.Exceptions.BookPublisherExceptions;
using BookPublisher.Application.Exceptions.ValidatorsExceptions;
using BookPublisher.Application.Interfaces;
using BookPublisher.Domain.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookPublisher.WebUI.Controllers;

[Authorize]
[Route("api/v{version:apiVersion}/authors")]
public class AuthorController : BookPublisherController
{
    private readonly IAuthorService _service; 
    private readonly IValidator<RegisterAuthorRequestJson> _validatorRegisterAuthor; 
    private readonly IValidator<UpdateAuthorRequestJson> _validatorUpdateAuthor;
    private readonly IMapper _mapper; 
    public AuthorController(IAuthorService service, 
        IValidator<RegisterAuthorRequestJson> validatorRegisterAuthor, 
        IValidator<UpdateAuthorRequestJson> validatorUpdateAuthor, 
        IMapper mapper)
    {
        _service = service; 
        _validatorRegisterAuthor = validatorRegisterAuthor; 
        _validatorUpdateAuthor = validatorUpdateAuthor;
        _mapper = mapper;  
    }

    [HttpGet]
    public ActionResult GetAuthorsAsync([FromQuery] AuthorParameters authorParameters)
    {
        var authors =  _service.GetAllAsync(authorParameters); 
        if(!authors.Any())
        {
            return NoContent();
        }

        var metadata = new 
        {
            authors.TotlaCount,
            authors.PageSize, 
            authors.CurrentPage,
            authors.TotalPages,
            authors.HasNext,
            authors.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
        var response = _mapper.Map<IEnumerable<AuthorResponseJson>>(authors);
        return Ok(response);

    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAuthorAsync(long id)
    {
        try 
        {
            var response = await _service.GetByIdAsync(id);
            return Ok(response);
        }
        catch(NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch
        {
            return BadRequest(new { message = "invalid request."});
        }
        
    }

    [HttpPost]
    public async Task<ActionResult> PostAuthorsAsync(RegisterAuthorRequestJson request)
    {
        var result = _validatorRegisterAuthor.Validate(request);
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }

        try 
        {
            var author = await _service.CreateAsync(request); 
            return StatusCode(201, author);
        }
        catch
        {
            return BadRequest(new { message = "invalid request."});
        }

        
    } 

    [HttpPut("{id}")]
    public async Task<ActionResult> PutAuthorsAsync(long id, UpdateAuthorRequestJson request)
    {
        var result = _validatorUpdateAuthor.Validate(request);
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        
        try 
        {
            await _service.UpdateAsync(id, request); 
            return NoContent(); 
        }
        catch(NotFoundException e)
        {
            return NotFound(new {message = e.Message});
        }
        catch
        {
            return BadRequest(new {message = "invalid request."});
        }

    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuthorAsync(long id)
    {
        try
        {
            await _service.RemoveAsync(id); 
            return NoContent(); 
        }
        catch(NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch
        {
            return BadRequest("invalid request.");
        }
    }
}
