using AutoMapper;
using BookPublisher.Application.Dtos.Author;
using BookPublisher.Application.Dtos.Book;
using BookPublisher.Domain.Entities;

namespace BookPublisher.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        RequestToEntity();
        EntityToRequest();
    }

    private void RequestToEntity()
    {
        CreateMap<RegisterAuthorRequestJson, Author>();
        CreateMap<UpdateAuthorRequestJson, Author>();
        CreateMap<RegisterAuthorIdRequestJson, Author>();

        CreateMap<RegisterBookRequestJson, Book>()
            .ConstructUsing(x => new Book(x.Title, x.SubTitle, x.Edition, x.LaunchDate, 
                x.Price, x.Quantity));

        CreateMap<RegisterBookRequestJson, Book>()
            .ConstructUsing(x => new Book(x.Title, x.Edition, x.LaunchDate, 
                x.Price, x.Quantity));
    
    }
    private void EntityToRequest()
    {
        CreateMap<Author, AuthorResponseJson>();

        CreateMap<Author, GetAuthorPerBookResponseJson>();
        
        CreateMap<Book, BookResponseJson>();
        CreateMap<Book, GetBooksResponseJson>();

        CreateMap<Book, GetBookResponseJson>(); 
    }
}
