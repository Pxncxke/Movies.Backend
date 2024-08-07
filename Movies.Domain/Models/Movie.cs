﻿using Movies.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Models;

public class Movie : BaseEntity
{
    public string Title { get; set; }
    public string Summary { get; set; }
    public bool InTheaters { get; set; }
    public string Trailer { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Poster { get; set; }
    public List<MoviesActors> MoviesActors { get; set; }
    public List<MoviesGenres> MoviesGenres { get; set; }
    public List<MovieTheatersMovies> MovieTheatersMovies { get; set; }
}
