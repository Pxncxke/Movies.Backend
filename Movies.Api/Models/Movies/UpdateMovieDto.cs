using Microsoft.AspNetCore.Mvc;
using Movies.Api.Models.Genres;
using Movies.Api.Models.MovieTheaters;

namespace Movies.Api.Models.Movies;

public class UpdateMovieDto 

{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public bool InTheaters { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Trailer { get; set; }
    public IFormFile Poster { get; set; }
    //[ModelBinder(BinderType = typeof(TypeBinder<List<Guid>>))]
    public List<Guid> GenresIds { get; set; }
    //[ModelBinder(BinderType = typeof(TypeBinder<List<Guid>>))]
    public List<Guid> MovieTheatersIds { get; set; }
    [ModelBinder(BinderType = typeof(TypeBinder<List<CreateMoviesActorsDto>>))]
    public List<CreateMoviesActorsDto> Actors { get; set; }
}


public class MovieToUpdateDto 
{
    public MovieDto Movie { get; set; }
    public List<ActorMovieDto> Actors { get; set; }
    public List<GenreDto> SelectedGenres { get; set; }
    public List<GenreDto> UnSelectedGenres { get; set; }
    public List<MovieTheaterDto> SelectedMovieTheaters { get; set; }
    public List<MovieTheaterDto> UnSelectedMovieTheaters { get; set; }
}