namespace Movies.Api.Models.MovieTheaters;

public class MovieTheaterDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
