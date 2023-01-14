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
                var listMovieDTO = new List<MovieDTO>();
                try
                {
                    listMovieDTO = await _movieExternalAPI.GetMovies();
                }
                catch (HttpRequestException)
                {
                    return null;
                }
                
                foreach (var movieDTO in listMovieDTO)
                {
                    if (await _context.Movies.AnyAsync(m => m.Title == movieDTO.Title)) continue;
                    await _context.AddAsync(_mapper.Map<Movie>(movieDTO));
                }
                await _context.SaveChangesAsync();

                return await _context.Movies.ToListAsync();
            }
        }
    }
}
