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
   
}
