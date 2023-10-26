using AutoMapper;
using BookPublisher.Application.Dtos.Author;
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
    }
    private void EntityToRequest()
    {
        CreateMap<Author, AuthorResponseJson>();
    }
}
