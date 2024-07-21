using System.ComponentModel.DataAnnotations;

namespace Movies.Api.Models.Ratings;

public class RatingDto
{
    [Range(1, 5)]
    public int Rating {  get; set; }
    public Guid MovieId { get; set; }
}
