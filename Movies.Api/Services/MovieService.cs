using AutoMapper;
using Movies.Api.Exceptions;
using Movies.Api.Interfaces;
using Movies.Api.Models;
using Movies.Api.Models.Actors;
using Movies.Api.Models.Genres;
using Movies.Api.Models.Movies;
using Movies.Api.Models.MovieTheaters;
using Movies.Api.Repositories;
using Movies.Domain.Models;

namespace Movies.Api.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IGenreRepository _genreRepository;
        private readonly IMovieTheaterRepository _movieTheaterRepository;
        private readonly IRatingRepository _ratingRepository;
        private string containerName = "movies";

        public MovieService(IMapper mapper, IMovieRepository movieRepository, IFileStorageService fileStorageService, 
            IGenreRepository genreRepository, IMovieTheaterRepository movieTheaterRepository,
            IRatingRepository ratingRepository)
        {
            this._mapper = mapper;
            this._movieRepository = movieRepository;
            this._fileStorageService = fileStorageService;
            this._genreRepository = genreRepository;
            this._movieTheaterRepository = movieTheaterRepository;
            this._ratingRepository = ratingRepository;
        }

        public async Task CreateMovieAsync(CreateMovieDto movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);

            if (movieDto.Poster != null)
            {
                movie.Poster = await _fileStorageService.SaveFile(containerName, movieDto.Poster);
            }

            movie.MoviesGenres = movieDto.GenresIds?.Select(x => new MoviesGenres { GenreId = x }).ToList();
            movie.MovieTheatersMovies = movieDto.MovieTheatersIds?.Select(x => new MovieTheatersMovies { MovieTheaterId = x }).ToList();
            movie.MoviesActors = movieDto.Actors?.Select(x => new MoviesActors { ActorId = x.Id, Character = x.Character }).ToList();
            if (movie.MoviesActors != null && movie.MoviesActors.Count > 0)
            {
                movie.MoviesActors.OrderBy(x => x.Order);
            }


            movie.ReleaseDate = movie.ReleaseDate.ToUniversalTime();
            //AnnotateActorsOrder(movie);

            await _movieRepository.CreateAsync(movie);
        }

        //private void AnnotateActorsOrder(Movie movie)
        //{
        //    if (movie.MoviesActors != null && movie.MoviesActors.Count > 0)
        //    {
        //        movie.MoviesActors.OrderBy(x => x.Order);
        //    }
        //}

        public async Task DeleteMovieAsync(Guid id)
        {
            await _movieRepository.DeleteAsync(id);
        }

        public Task<List<MovieDto>> GetAllMoviesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<HomeDto> GetHomeMoviesAsync(int recordsToReturn, DateTime topDate)
        {
            var response = new HomeDto();

            var inTheaters = await _movieRepository.GetInTheaters(recordsToReturn);
            var upcomingReleases = await _movieRepository.GetUpComingReleases(recordsToReturn, topDate);

            response.InTheaters = _mapper.Map<List<MovieDto>>(inTheaters);
            response.UpcomingReleases = _mapper.Map<List<MovieDto>>(upcomingReleases);
            

            return response;
        }

        public async Task<MovieToUpdateDto> GetMovieByIdAsync(Guid id)
        {
            var movie = await _movieRepository.GetMovieWithDependenciesAsync(id) ?? throw new NotFoundException(nameof(Movie), id);

            var selectedGenres = movie.MoviesGenres.Select(x => new GenreDto { Id = x.Genre.Id, Name = x.Genre.Name }).ToList();
            var genres = await _genreRepository.GetAsync();
            var unSelectedGenres = genres.Where(x => !selectedGenres.Contains(new GenreDto { Id = x.Id, Name = x.Name })).ToList();

            var selectedTheaters = movie.MovieTheatersMovies.Select(x => new MovieTheaterDto { Id = x.MovieTheater.Id, Name = x.MovieTheater.Name }).ToList();
            var theaters = await _movieTheaterRepository.GetAsync();
            var unSelectedTheaters = theaters.Where(x => !selectedTheaters.Contains(new MovieTheaterDto { Id = x.Id, Name = x.Name })).ToList();

            //var selectedGenresDto = _mapper.Map<List<GenreDto>>(selectedGenres);
            var unselectedGenresDto = _mapper.Map<List<GenreDto>>(unSelectedGenres);
            //var selectedTheatersDto = _mapper.Map<List<MovieTheaterDto>>(selectedTheaters);
            var unselectedTheatersDto = _mapper.Map<List<MovieTheaterDto>>(unSelectedTheaters);



            var dto = new MovieToUpdateDto
            {
                Movie = _mapper.Map<MovieDto>(movie),
                SelectedGenres = selectedGenres,
                UnSelectedGenres = unselectedGenresDto,
                SelectedMovieTheaters = selectedTheaters,
                UnSelectedMovieTheaters = unselectedTheatersDto,
                Actors = movie.MoviesActors.Select(x => new ActorMovieDto
                {
                    Id = x.Actor.Id,
                    Name = x.Actor.Name,
                    Picture = x.Actor.Picture,
                    Character = x.Character,
                    Order = x.Order
                }).ToList()
            };


            return dto;
        }

        public async Task<Models.PagedList<MovieDto>> GetMoviesWithPaginationAsync(string? searchTitle, bool? inTheaters, bool? upcomingReleases, string? searchGenre, string? sortOrder, int page, int pageSize)
        {
            var movies = await _movieRepository.GetMoviesWithPaginationAsync(searchTitle, inTheaters, upcomingReleases, searchGenre,sortOrder, page, pageSize);


            var dto = _mapper.Map<List<MovieDto>>(movies.Items);

            var result = new PagedList<MovieDto>(dto, movies.Page, movies.PageSize, movies.TotalCount);

            return result;
        }

        public async Task<MovieDto> GetMovieWithDependenciesAsync(Guid id)
        {
            var movie = await _movieRepository.GetMovieWithDependenciesAsync(id);

            var dto = _mapper.Map<MovieDto>(movie);

            dto.Actors = movie.MoviesActors?.Select(x => new ActorMovieDto
            {
                Id = x.Actor.Id,
                Name = x.Actor.Name,
                Picture = x.Actor.Picture,
                Character = x.Character,
                Order = x.Order
            }).ToList();

            dto.Genres = movie.MoviesGenres?.Select(x => new GenreDto
            {
                Id = x.Genre.Id,
                Name = x.Genre.Name
            }).ToList();

            dto.MovieTheaters = movie.MovieTheatersMovies?.Select(x => new MovieTheaterDto
            {
                Id = x.MovieTheater.Id,
                Name = x.MovieTheater.Name,
                Latitude = x.MovieTheater.Location.Y,
                Longitude = x.MovieTheater.Location.X
            }).ToList();

           

            var avergaVote = await _ratingRepository.GetAverageRatingAsync(id);
            var userVote = await _ratingRepository.GetAverageUsersAsync(id);

            dto.AverageVote = avergaVote;
            dto.UserVote = userVote;
            dto.Actors.OrderBy(x => x.Order);
            return dto;
        }

        public async Task UpdateMovieAsync(UpdateMovieDto movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);
            var previous = await _movieRepository.GetByIdAsync(movie.Id);

            if (movieDto.Poster != null)
            {
                movie.Poster = await _fileStorageService.EditFile(containerName, movieDto.Poster, previous.Poster);
            }

           

            if (movie.MoviesActors != null && movie.MoviesActors.Count > 0)
            {
                movie.MoviesActors.OrderBy(x => x.Order);
            }


            movie.ReleaseDate = movie.ReleaseDate.ToUniversalTime();
            movie.CreatedAt = previous.CreatedAt;
            await _movieRepository.UpdateAsync(movie);
        }
    }
}
