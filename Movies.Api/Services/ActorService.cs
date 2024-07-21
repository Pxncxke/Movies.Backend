using AutoMapper;
using Movies.Api.Exceptions;
using Movies.Api.Interfaces;
using Movies.Api.Models;
using Movies.Api.Models.Actors;
using Movies.Api.Models.Genres;
using Movies.Api.Repositories;
using Movies.Domain.Models;

namespace Movies.Api.Services;

public class ActorService : IActorService
{
    private readonly IMapper _mapper;
    private readonly IActorRepository _actorRepository;
    private readonly IFileStorageService fileStorageService;
    private string containerName = "actors";

    public ActorService(IMapper mapper, IActorRepository actorRepository, IFileStorageService fileStorageService)
    {
        this._mapper = mapper;
        this._actorRepository = actorRepository;
        this.fileStorageService = fileStorageService;
    }

    public async Task CreateActorAsync(CreateActorDto actor)
    {
        var actorEntity = _mapper.Map<Actor>(actor);

        if (actor.Picture != null)
        {
            actorEntity.Picture = await fileStorageService.SaveFile(containerName, actor.Picture);
        }
        actorEntity.DateOfBirth = actor.DateOfBirth.Value.ToUniversalTime();

        await _actorRepository.CreateAsync(actorEntity);
    }

    public async Task DeleteActorAsync(Guid id)
    {
        await _actorRepository.DeleteAsync(id);
    }

    public async Task<ActorDto> GetActorByIdAsync(Guid id)
    {
        var actor = await _actorRepository.GetByIdAsync(id) ?? throw new NotFoundException(nameof(Actor), id);

        var dto = _mapper.Map<ActorDto>(actor);

        dto.DateOfBirth = dto.DateOfBirth?.ToLocalTime();

        return dto;
    }

    public async Task<List<ActorsMovieDto>> GetActorByNameAsync(string name)
    {
        var actor = await _actorRepository.SearchActorsByName(name) ?? throw new NotFoundException(nameof(Actor), name);

        var dto = _mapper.Map<List<ActorsMovieDto>>(actor);

        return dto;
    }

    public async Task<PagedList<ActorDto>> GetActorsWithPaginationAsync(string? search, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        var genres = await _actorRepository.GetActorsWithPaginationAsync(search, sortColumn, sortOrder, page, pageSize);


        var dto = _mapper.Map<List<ActorDto>>(genres.Items);

        var result = new PagedList<ActorDto>(dto, genres.Page, genres.PageSize, genres.TotalCount);

        return result;
    }

    public async Task<List<ActorDto>> GetAllActorsAsync()
    {
        var actors = await _actorRepository.GetAsync();
        var result = _mapper.Map<List<ActorDto>>(actors);
        return result;
    }

    public async Task UpdateActorAsync(UpdateActorDto actorDto)
    {
        var actorEntity = _mapper.Map<Actor>(actorDto);
        var previous = await _actorRepository.GetByIdAsync(actorEntity.Id);

        if(actorDto.Picture == null && actorDto.PictureUrl == null)
        {
            actorEntity.Picture = previous.Picture;
        }
        else if(actorDto.Picture == null && actorDto.PictureUrl != null)
        {
            actorEntity.Picture = actorDto.PictureUrl;
        }
     
        if(actorDto.Picture != null)
        {
            actorEntity.Picture = await fileStorageService.EditFile(containerName, actorDto.Picture, actorEntity.Picture);
        }


        actorEntity.DateOfBirth = actorDto.DateOfBirth?.ToUniversalTime();

        actorEntity.CreatedAt = previous.CreatedAt;

        await _actorRepository.UpdateAsync(actorEntity);
    }
}
