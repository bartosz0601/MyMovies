using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Movies
{
    public class Edit
    {
        public class Command : IRequest<Movie>
        {
            public Movie Movie;
        }
        public class Handler : IRequestHandler<Command, Movie>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Movie> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _context.Movies
                    .AnyAsync(m => m.Title == request.Movie.Title && m.Id != request.Movie.Id))
                {
                    return null;
                }
                var movie = await _context.Movies.FindAsync(request.Movie.Id);
                _mapper.Map(request.Movie, movie);
                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return null;
                return movie;
            }
        }
    }
}