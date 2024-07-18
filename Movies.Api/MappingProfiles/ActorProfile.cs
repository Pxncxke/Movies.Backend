using AutoMapper;
using Movies.Api.Models.Actors;
using Movies.Domain.Models;

namespace Movies.Api.MappingProfiles;

public class ActorProfile : Profile
{
    public ActorProfile()
    {
        CreateMap<ActorDto, Actor>().ReverseMap();
        CreateMap<CreateActorDto, Actor>().ForMember(x => x.Picture, options => options.Ignore()).ReverseMap();
        CreateMap<UpdateActorDto, Actor>().ReverseMap();
    }
}