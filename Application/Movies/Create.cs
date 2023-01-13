using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Movies
{
    public class Create
    {
        public class Command : IRequest<Movie>
        {
            public Movie Movie;
        }
        public class Handler : IRequestHandler<Command, Movie>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Movie> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _context.Movies.AnyAsync(m => m.Title == request.Movie.Title))
                {
                    return null;
                }
                //request.Movie.Id= Guid.NewGuid();
                await _context.Movies.AddAsync(request.Movie);
                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return null;
                return request.Movie;
            }
        }
    }
}
