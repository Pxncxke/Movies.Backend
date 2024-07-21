using AutoMapper;
using Movies.Api.Exceptions;
using Movies.Api.Interfaces;
using Movies.Api.Models;
using Movies.Api.Models.Genres;
using Movies.Api.Models.MovieTheaters;
using Movies.Api.Repositories;
using Movies.Domain.Models;

namespace Movies.Api.Services;

public class MovieTheaterService : IMovieTheaterService
{
    private readonly IMapper mapper;
    private readonly IMovieTheaterRepository movieTheaterRepository;

    public MovieTheaterService(IMapper mapper, IMovieTheaterRepository movieTheaterRepository)
    {
        this.mapper = mapper;
        this.movieTheaterRepository = movieTheaterRepository;
    }

    public async Task CreateMovieTheaterAsync(CreateMovieTheaterDto movieTheaterDto)
    {

        var movieTheater = mapper.Map<MovieTheater>(movieTheaterDto);

        await movieTheaterRepository.CreateAsync(movieTheater);
    }

    public async Task DeleteMovieTheaterAsync(Guid id)
    {
        await movieTheaterRepository.DeleteAsync(id);
    }

    public async Task<List<MovieTheaterDto>> GetAllMovieTheatersAsync()
    {
        var movieTheaters = await movieTheaterRepository.GetAsync();
        var result = mapper.Map<List<MovieTheaterDto>>(movieTheaters);
        return result;
    }

    public async Task<MovieTheaterDto> GetMovieTheaterByIdAsync(Guid id)
    {
        var movieTheater = await movieTheaterRepository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(MovieTheater), id);

        var dto = mapper.Map<MovieTheaterDto>(movieTheater);

        return dto;
    }

    public async Task<PagedList<MovieTheaterDto>> GetMovieTheatersWithPaginationAsync(string? search, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var movieTheater = await movieTheaterRepository.GetMovieTheatersWithPaginationAsync(search, sortColumn, sortOrder, page, pageSize);


        var dto = mapper.Map<List<MovieTheaterDto>>(movieTheater.Items);

        var result = new PagedList<MovieTheaterDto>(dto, movieTheater.Page, movieTheater.PageSize, movieTheater.TotalCount);

        return result;
    }

    public async Task UpdateMovieTheaterAsync(UpdateMovieTheaterDto movieTheaterDto)
    {
        var movieTheater = mapper.Map<MovieTheater>(movieTheaterDto);
        var previous = await movieTheaterRepository.GetByIdAsync(movieTheater.Id);
        movieTheater.CreatedAt = previous.CreatedAt;
        await movieTheaterRepository.UpdateAsync(movieTheater);
    }
}
