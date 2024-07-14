using AutoMapper;
using Movies.Api.Exceptions;
using Movies.Api.Interfaces;
using Movies.Api.Models.Genres;
using Movies.Api.Validations.Genres;
using Movies.Domain.Models;

namespace Movies.Api.Services;

public class GenreService : IGenreService
{
    private readonly IMapper _mapper;
    private readonly IGenreRepository _genreRepository;

    public GenreService(IMapper mapper, IGenreRepository genreRepository)
    {
        this._mapper = mapper;
        this._genreRepository = genreRepository;
    }

    public async Task CreateGenreAsync(CreateGenreDto request)
    {
        var validator = new CreateGenreValidator(_genreRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid) 
        {
            throw new BadRequestException("Invalid Genre", validationResult);
        }

        var genre = _mapper.Map<Genre>(request);

        await _genreRepository.CreateAsync(genre);
    }

    public Task DeleteGenreAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<GenreDto>> GetAllGenresAsync()
    {
        var genres = await _genreRepository.GetAsync();

        var dto = _mapper.Map<List<GenreDto>>(genres);

        return dto;
    }

    public async Task<GenreDto> GetGenreByIdAsync(Guid id)
    {
        var genre = await _genreRepository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Genre), id);

        var dto = _mapper.Map<GenreDto>(genre);

        return dto;
    }

    public Task UpdateGenreAsync(CreateGenreDto genre)
    {
        throw new NotImplementedException();
    }
}
