using System.Text.Json;
using AutoMapper;
using BookPublisher.Application.Dtos.Book;
using BookPublisher.Application.Exceptions.BookPublisherExceptions;
using BookPublisher.Application.Exceptions.ValidatorsExceptions;
using BookPublisher.Application.Interfaces;
using BookPublisher.Domain.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookPublisher.WebUI.Controllers;

[Authorize]
[Route("api/v{version:apiVersion}/books")]
public class BookController : BookPublisherController
{
    private readonly IBookService _service;
    private readonly IMapper _mapper;
    private readonly IValidator<RegisterBookRequestJson> _validatorRegisterBook;
    public BookController(IBookService service, 
        IValidator<RegisterBookRequestJson> validatorRegisterBook, 
        IMapper mapper)
    {
        _service = service; 
        _validatorRegisterBook = validatorRegisterBook;
        _mapper = mapper; 
    }    

    [HttpPost]
    public async Task<ActionResult> PostBook(RegisterBookRequestJson request)
    {
        var result = _validatorRegisterBook.Validate(request); 
        if(!result.IsValid)
        {
            return BadRequest(result.Errors.ToCustomValidationFailure());
        }
        try 
        {
            var response = await _service.CreateAsync(request);
            return StatusCode(201, response);
        }
        catch(NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch(AuthorRequiredException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch(RepeatedIdException e)
        {
            return BadRequest(new { message = e.Message });
        }
        catch 
        {
            return BadRequest(new { message = "Invalid request/internal error server" });
        }
    }

    [HttpGet]
    public ActionResult GetAllBooks([FromQuery] BookParameters bookParameters)
    {
        var books = _service.GetAllAsync(bookParameters);
        if(!books.Any())
        {
            return NoContent();
        }

        var metadata = new 
        {
            books.TotlaCount,
            books.PageSize,
            books.CurrentPage,
            books.TotalPages,
            books.HasPrevious,
            books.HasNext
        };

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
        var response = _mapper.Map<IEnumerable<GetBookResponseJson>>(books);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetBook(long id)
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
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBook(UpdateBookRequestJson request, long id)
    {
        try 
        {
            await _service.UpdateAsync(request, id);
            return NoContent(); 
        }   
        catch(NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch
        {
            return BadRequest(new { message = "Invalid request/internal error server"});
        }
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBook(long id)
    {
        try 
        {   
            await _service.DeleteAsync(id); 
            return NoContent(); 
        }
        catch(NotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch
        {
            return BadRequest( new { message ="Invalid request/internal error server" });
        }
    }
   
}
