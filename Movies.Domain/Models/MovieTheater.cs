using Movies.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Models;

public class MovieTheater : BaseEntity
{
    public string Name { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
}
