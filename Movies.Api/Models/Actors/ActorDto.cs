namespace Movies.Api.Models.Actors;

public class ActorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Biography { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Picture { get; set; }
}