namespace Movies.Api.Models.Movies
{
    public class HomeDto
    {
        public List<MovieDto> UpcomingReleases { get; set; }
        public List<MovieDto> InTheaters { get; set; }
    }
}
