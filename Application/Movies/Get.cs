using Domain;
using MediatR;
using Persistence;

namespace Application.Movies
{
    public class Get
    {
        public class Query : IRequest<Movie>
        {
            public Guid Id; 
        }

        public class Handler : IRequestHandler<Query, Movie>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Movie> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Movies.FindAsync(request.Id);
            }
        }
    }
}
