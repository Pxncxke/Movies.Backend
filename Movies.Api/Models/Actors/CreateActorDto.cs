namespace Movies.Api.Models.Actors;

public class CreateActorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Biography { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public IFormFile? Picture { get; set; }
}