namespace Movies.Api.Models.Actors;

public class ActorsMovieDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
    public string Character { get; set; }
    public int Order { get; set; }
}
