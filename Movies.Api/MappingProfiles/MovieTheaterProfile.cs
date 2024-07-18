using AutoMapper;
using Movies.Api.Models.MovieTheaters;
using Movies.Domain.Models;
using NetTopologySuite.Geometries;

namespace Movies.Api.MappingProfiles;

public class MovieTheaterProfile : Profile
{
    public MovieTheaterProfile()
    {
        CreateMap<MovieTheater, MovieTheaterDto>()
            .ForMember(x => x.Latitude, dto => dto.MapFrom(prop => prop.Location.Y))
            .ForMember(x => x.Longitude, dto => dto.MapFrom(prop => prop.Location.X));
        CreateMap<CreateMovieTheaterDto, MovieTheater>()
            .ForMember(x => x.Location, x => x.MapFrom(dto => new Point(dto.Longitude, dto.Latitude)));
        CreateMap<UpdateMovieTheaterDto, MovieTheater>()
    .ForMember(x => x.Location, x => x.MapFrom(dto => new Point(dto.Longitude, dto.Latitude)));
        //CreateMap<CreateMovieTheaterDto, MovieTheater>()
        //    .ForMember(x => x.Location, x => x.MapFrom(dto => geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));
        //CreateMap<UpdateMovieTheaterDto, MovieTheater>()
        //    .ForMember(x => x.Location, x => x.MapFrom(dto => geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));
    }
}