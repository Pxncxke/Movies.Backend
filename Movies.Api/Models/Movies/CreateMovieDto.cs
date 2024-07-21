using Microsoft.AspNetCore.Mvc;

namespace Movies.Api.Models.Movies;

public class CreateMovieDto
{
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
//public class CreateActorMovieDto
//{
//   public Guid Id { get; set; }
//   public string Character { get; set; }
//}