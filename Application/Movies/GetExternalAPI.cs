using Application.ExternalAPI;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Movies
{
    public class GetExternalAPI
    {
        public class Query : IRequest<List<Movie>> 
        {
        }

        public class Handler : IRequestHandler<Query, List<Movie>>
        {
            private readonly DataContext _context;
            private readonly IMovieExternalAPI _movieExternalAPI;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMovieExternalAPI movieExternalAPI, IMapper mapper)
            {
                _context = context;
                _movieExternalAPI = movieExternalAPI;
                _mapper = mapper;
            }

            public async Task<List<Movie>> Handle(Query request, CancellationToken cancellationToken)
            {
                var listMovieDTO = new List<MovieExtDTO>();
                try
                {
                    listMovieDTO = await _movieExternalAPI.GetMovies(cancellationToken);
                }
                catch (HttpRequestException)
                {
                    return null;
                }

                var listNewMovie = new List<Movie>();

                foreach (var movieDTO in listMovieDTO)
                {
                    if (await _context.Movies.AnyAsync(m => m.Title == movieDTO.Title)) continue;
                    listNewMovie.Add(_mapper.Map<Movie>(movieDTO));
                }

                await _context.AddRangeAsync(listNewMovie);
                await _context.SaveChangesAsync();

                return listNewMovie;
            }
        }
    }
}
