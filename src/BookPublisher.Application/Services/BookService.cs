using System.Runtime.CompilerServices;
using AutoMapper;
using BookPublisher.Application.Dtos.Book;
using BookPublisher.Application.Exceptions.BookPublisherExceptions;
using BookPublisher.Application.Interfaces;
using BookPublisher.Domain.Entities;
using BookPublisher.Domain.Interfaces;

namespace BookPublisher.Application.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IAuthorPerBookRepository _aPerBookRepository;
    private readonly IMapper _mapper;
    public BookService(IBookRepository repository, IAuthorRepository authorRepository, 
        IMapper mapper, IAuthorPerBookRepository aPerBookRepository)
    {
        _repository = repository;
        _authorRepository = authorRepository;
        _aPerBookRepository = aPerBookRepository; 
        _mapper = mapper;  
    }

    public async Task<BookResponseJson> CreateAsync(RegisterBookRequestJson request)
    {
        if(!request.AuthorsId.Any())
        {
            throw new AuthorRequiredException("author is required.");
        }
        var authorsIds = await ListAuthors(request.AuthorsId);
        var isDistinct = NoRepeatId(authorsIds);  
        if(!isDistinct)
        {
            throw new RepeatedIdException("ids entered are the same");
        }
        var book = _mapper.Map<Book>(request); 
        await _repository.CreateAsync(book); 
        await RegisterAuthorId(authorsIds, book.Id); 
        
        var response = _mapper.Map<BookResponseJson>(book);
        return response; 

    }

    public async Task<IEnumerable<GetBooksResponseJson>> GetAllAsync()
    {
        var books = await _repository.GetAllAsync(); 
        var response = _mapper.Map<IEnumerable<GetBooksResponseJson>>(books);
        return response;
    }

    public async Task<GetBookResponseJson> GetByIdAsync(long id)
    {
        var book = await _repository.GetByIdAsync(id); 
        if(book is null)
        {
            throw new NotFoundException("book not found.");
        }
        var response = _mapper.Map<GetBookResponseJson>(book);
        return response;
    }
    public async Task DeleteAsync(long id)
    {
        var book = await _repository.GetByIdAsync(id);
        if(book is null)
        {
            throw new NotFoundException("book not found.");
        }
        await _repository.RemoveAsync(book);
    }
    private async Task<List<long>> ListAuthors(List<RegisterAuthorIdRequestJson> authorsIds)
    {
        var list = new List<long>();
        foreach(var author in authorsIds)
        {
            var result = await _authorRepository.GetByIdAsync(author.Id);
            if(result is null)
            {
                throw new NotFoundException("author not found.");
            }
            list.Add(result.Id); 
        }
        return list; 
    }

    private async Task RegisterAuthorId(List<long> ids, long bookId) 
    {
        foreach(var id in ids)
        {
            var row = new AuthorPerBook(id, bookId);
            await _aPerBookRepository.CreateAsyn(row); 
        }
    }

    private bool NoRepeatId(List<long> ids)
    {
        var isDistinct = ids.GroupBy(x => x).All(g => g.Count() == 1);
        return isDistinct;
    }
}
