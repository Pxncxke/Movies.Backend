using Movies.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;


namespace Movies.Domain.Models;

public class MovieTheater : BaseEntity
{
    public string Name { get; set; }
    [Column(TypeName = "geometry (point)")]
    public Point? Location { get; set; }
}
