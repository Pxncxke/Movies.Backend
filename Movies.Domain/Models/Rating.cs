using Movies.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Models;

public class Rating : BaseEntity
{
    [Range(1, 5)]
    public int Rate { get; set; }
    [ForeignKey("Movie")]
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; }
}
