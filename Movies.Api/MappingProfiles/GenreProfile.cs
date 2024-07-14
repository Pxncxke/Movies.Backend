using AutoMapper;
using Movies.Api.Models.Genres;
using Movies.Domain.Models;

namespace Movies.Api.MappingProfiles
{
    public class GenreProfile : Profile
    {
        public GenreProfile()
        {
            CreateMap<GenreDto, Genre>().ReverseMap();
            CreateMap<CreateGenreDto, Genre>().ReverseMap();
            CreateMap<GenreDto, Genre>().ReverseMap();
        }
    }
}
