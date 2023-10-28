using BookPublisher.Application.Dtos.Book;
using BookPublisher.Application.Exceptions.BookPublisherExceptions;
using BookPublisher.Application.Exceptions.ValidatorsExceptions;
using BookPublisher.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookPublisher.WebUI.Controllers;

[Route("api/v{version:apiVersion}/books")]
public class BookController : BookPublisherController
{
    private readonly IBookService _service;
    private readonly IValidator<RegisterBookRequestJson> _validatorRegisterBook;
    public BookController(IBookService service, 
        IValidator<RegisterBookRequestJson> validatorRegisterBook)
    {
        _service = service; 
        _validatorRegisterBook = validatorRegisterBook; 
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
    public async Task<ActionResult> GetAllBooks()
    {
        var response = await _service.GetAllAsync(); 
        if(!response.Any())
        {
            return NoContent();
        }
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
            return BadRequest("Invalid request/internal error server");
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
            return BadRequest("Invalid request/internal error server");
        }
    }
   
}
