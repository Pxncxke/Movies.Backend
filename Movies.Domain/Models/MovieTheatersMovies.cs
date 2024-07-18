using Movies.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Models;

public class MovieTheatersMovies : BaseEntity
{
    public Guid MovieId { get; set; }
    public Guid MovieTheaterId { get; set; }
    public Movie Movie { get; set; }
    public MovieTheater MovieTheater { get; set; }
}
