using AutoMapper;
using BookPublisher.Application.Dtos.Author;
using BookPublisher.Application.Exceptions.BookPublisherExceptions;
using BookPublisher.Application.Interfaces;
using BookPublisher.Domain.Entities;
using BookPublisher.Domain.Interfaces;

namespace BookPublisher.Application.Services;

public class AuthorService : IAuthorService
{

    private readonly IAuthorRepository _repository;
    private readonly IMapper _mapper; 
    public AuthorService(IAuthorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper; 
    }
    public async Task<AuthorResponseJson> CreateAsync(RegisterAuthorRequestJson request)
    {
        var author = _mapper.Map<Author>(request);
        var response = await _repository.CreateAsync(author);
        return _mapper.Map<AuthorResponseJson>(response); 

    }

    public async Task<IEnumerable<AuthorResponseJson>> GetAllAsync()
    {
        var response = await _repository.GetAllAsync(); 
        return _mapper.Map<IEnumerable<AuthorResponseJson>>(response);
    }

    public async Task<AuthorResponseJson> GetByIdAsync(long id)
    {
        var author = await _repository.GetByIdAsync(id);
        if(author is null)
        {
            throw new NotFoundException("author not found.");
        }
        var response = _mapper.Map<AuthorResponseJson>(author); 
        return response; 
    }

    public async Task RemoveAsync(long id)
    {
        var author = await _repository.GetByIdAsync(id);
        if(author is null)
        {
            throw new NotFoundException("author not found.");
        }
        await _repository.DeleteAsync(author); 
    }

    public async Task UpdateAsync(long id, UpdateAuthorRequestJson request)
    {
        var author = await _repository.GetByIdTracking(id); 
        if(author is null)
        {
            throw new NotFoundException("author not found.");
        }
        _mapper.Map(request, author);
        await _repository.UpdateAsync();
    }
}
