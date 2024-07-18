using FluentValidation;
using Movies.Api.Interfaces;
using Movies.Api.Models.Genres;

namespace Movies.Api.Validations.Genres;

public class UpdateGenreValidator : AbstractValidator<UpdateGenreDto>
{
    private readonly IGenreRepository _genreRepository;

    public UpdateGenreValidator(IGenreRepository genreRepository)
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Missing Id")
            .MustAsync(MustExist).WithMessage("Id does not exist");

        RuleFor(x => x.Name)
            .NotNull().WithMessage("{PropertyName} is required")
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .MaximumLength(50).WithMessage("{PropertyName} must be fewer than 50 characters")
            .Matches(@"^(?:[A-Z][a-z]+ ?)*$").WithMessage("{PropertyName} has an invalid format")
            .MustAsync(MustBeUniqueNameAsync).WithMessage("{PropertyName} already exists");

        this._genreRepository = genreRepository;
    }

    private async Task<bool> MustExist(Guid id, CancellationToken token)
    {
        var genre = await _genreRepository.GetByIdAsync(id);

        return genre != null;
    }

    private async Task<bool> MustBeUniqueNameAsync(string name, CancellationToken token)
    {
        var genre = await _genreRepository.GetByNameAsync(name);

        return genre == null;
    }
}