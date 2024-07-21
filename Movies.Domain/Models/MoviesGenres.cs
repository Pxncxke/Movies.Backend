using Movies.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Models;

public class MoviesGenres : BaseEntity
{
    [ForeignKey("Movie")]
    public Guid MovieId { get; set; }
    [ForeignKey("Genre")]
    public Guid GenreId { get; set; }
    public Movie Movie { get; set; }
    public Genre Genre { get; set; }
}
