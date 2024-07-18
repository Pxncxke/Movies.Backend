namespace Movies.Api.Models.Actors;

public class UpdateActorDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Biography { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public IFormFile? Picture { get; set; }
    public string PictureUrl { get; set; }
}
