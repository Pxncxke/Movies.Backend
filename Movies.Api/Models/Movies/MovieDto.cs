using Movies.Api.Models.Genres;
using Movies.Api.Models.MovieTheaters;

namespace Movies.Api.Models.Movies;

public class MovieDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Summary { get; set; }
    public bool InTheaters { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Trailer { get; set; }
    public string Poster { get; set; }
    public List<GenreDto> Genres { get; set; }
    public List<MovieTheaterDto> MovieTheaters { get; set; }
    public List<ActorMovieDto> Actors { get; set; }
    public decimal AverageVote { get; set; }
    public int UserVote { get; set; }
}

public class ActorMovieDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Character { get; set; }
    public string Picture { get; set; }
    public int Order { get; set; }
}

//public class MovieTheaterDto
//{
//    public Guid Id { get; set; }
//    public string Name { get; set; }
//    public double Latitude { get; set; }
//    public double Longitude { get; set; }
//    public List<MovieDto> Movies { get; set; }
//}

//public class GenreDto
//{
//    public Guid Id { get; set; }
//    public string Name { get; set; }
//}