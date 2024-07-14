using FluentValidation;
using Movies.Api.Interfaces;
using Movies.Api.Models.Genres;

namespace Movies.Api.Validations.Genres
{
    public class CreateGenreValidator : AbstractValidator<CreateGenreDto>
    {
        private readonly IGenreRepository _genreRepository;

        public CreateGenreValidator(IGenreRepository genreRepository)
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Missing Id")
                .MustAsync(MustNotExist).WithMessage("Id already exists");

            RuleFor(x => x.Name)
                .NotNull().WithMessage("{PropertyName} is required")
                .NotEmpty().WithMessage("{PropertyName} cannot be empty")
                .MaximumLength(50).WithMessage("{PropertyName} must be fewer than 50 characters")
                .Matches(@"^[A-Z][a-z]*$").WithMessage("{PropertyName} should only have the first letter in uppercase")
                .MustAsync(MustBeUniqueNameAsync).WithMessage("{PropertyName} already exists");


            this._genreRepository = genreRepository;
        }

        private async Task<bool> MustNotExist(Guid id, CancellationToken token)
        {
            var genre = await _genreRepository.GetByIdAsync(id);

            return genre == null;
        }

        private async Task<bool> MustBeUniqueNameAsync(string name, CancellationToken token)
        {
            var genre = await _genreRepository.GetByNameAsync(name);

            return genre == null;
        }
    }
}
