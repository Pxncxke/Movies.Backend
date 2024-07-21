using AutoMapper;
using Movies.Api.Models.Movies;
using Movies.Domain.Models;

namespace Movies.Api.MappingProfiles;

public class MovieProfile : Profile
{
    public MovieProfile()
    {
        CreateMap<MovieDto, Movie>().ReverseMap();
        CreateMap<CreateMovieDto, Movie>()
            .ForMember(x => x.Poster, options => options.Ignore());
            //.ForMember(x => x.MoviesGenres, options => options.MapFrom(MapMovieGenres))
            //.ForMember(x => x.MovieTheatersMovies, options => options.MapFrom(MapMovieGenres))
            //.ForMember(x => x.MoviesActors, options => options.MapFrom(MapMovieActors));
        CreateMap<UpdateMovieDto, Movie>().ForMember(x => x.Poster, options => options.Ignore());
        CreateMap<CreateMoviesActorsDto, MoviesActors>().ReverseMap();
        CreateMap<ActorMovieDto, Actor>().ReverseMap();

    }

    private List<MoviesGenres> MapMovieGenres(CreateMovieDto movieDto, Movie movie)
    {
        var result = new List<MoviesGenres>();
        if (movieDto.GenresIds == null) { return result; }

        foreach (var id in movieDto.GenresIds)
        {
            result.Add(new MoviesGenres() { GenreId = id });
        }

        return result;
    }

    private List<MovieTheatersMovies> MapMovieTheaters(CreateMovieDto movieDto, Movie movie)
    {
        var result = new List<MovieTheatersMovies>();
        if (movieDto.MovieTheatersIds == null) { return result; }

        foreach (var id in movieDto.MovieTheatersIds)
        {
            result.Add(new MovieTheatersMovies() { MovieTheaterId = id });
        }

        return result;
    }

    private List<MoviesActors> MapMovieActors(CreateMovieDto movieDto, Movie movie)
    {
        var result = new List<MoviesActors>();
        if (movieDto.Actors == null) { return result; }

        foreach (var actor in movieDto.Actors)
        {
            result.Add(new MoviesActors() { ActorId = actor.Id, Character = actor.Character });
        }

        return result;
    }
}