using FluentValidation;
using Movies.Api.Interfaces;

namespace Movies.Api.Validations.Genres;

public class DeleteGenreValidator : AbstractValidator<Guid>
{
    private readonly IGenreRepository _genreRepository;

    public DeleteGenreValidator(IGenreRepository genreRepository)
    {
        RuleFor(x => x)
            .MustAsync(MustExist).WithMessage("Genre does not exist");

        this._genreRepository = genreRepository;
    }

    private async Task<bool> MustExist(Guid id, CancellationToken token)
    {
        var genre = await _genreRepository.GetByIdAsync(id);

        return genre != null;
    }
}
