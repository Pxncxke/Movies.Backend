using AutoMapper;
using Movies.Api.Exceptions;
using Movies.Api.Interfaces;
using Movies.Api.Models;
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

    public async Task DeleteGenreAsync(Guid id)
    {
        var validator = new DeleteGenreValidator(_genreRepository);
        var validationResult = await validator.ValidateAsync(id);
        if (!validationResult.IsValid)
        {
            throw new NotFoundException(nameof(Genre), id);
        }
        await _genreRepository.DeleteAsync(id);
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

    public async Task<PagedList<GenreDto>> GetGenresWithPaginationAsync(string? search, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var genres = await _genreRepository.GetGenresWithPaginationAsync(search, sortColumn, sortOrder, page, pageSize);

     
        var dto = _mapper.Map<List<GenreDto>>(genres.Items);

        var result = new PagedList<GenreDto>(dto, genres.Page, genres.PageSize, genres.TotalCount);

        return result;
    }

    public async Task UpdateGenreAsync(UpdateGenreDto genreDto)
    {
        var validator = new UpdateGenreValidator(_genreRepository);
        var validationResult = await validator.ValidateAsync(genreDto);
        if (!validationResult.IsValid) 
        {
            throw new BadRequestException("Invalid Genre", validationResult);
        }
        var genre = _mapper.Map<Genre>(genreDto);
        var previous = await _genreRepository.GetByIdAsync(genre.Id);
        genre.CreatedAt = previous.CreatedAt;
        await _genreRepository.UpdateAsync(genre);
    }
}
