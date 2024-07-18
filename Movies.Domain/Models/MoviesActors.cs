using Movies.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Models;

public class MoviesActors : BaseEntity
{
    public Guid MovieId { get; set; }
    public Guid ActorId { get; set; }
    public string Character { get; set; }
    public int Order { get; set; }
    public Movie Movie { get; set; }
    public Actor Actor { get; set; }
}
