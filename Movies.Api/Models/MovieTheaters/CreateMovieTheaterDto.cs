﻿namespace Movies.Api.Models.MovieTheaters;

public class CreateMovieTheaterDto
{
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}