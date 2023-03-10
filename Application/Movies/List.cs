using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Movies
{
    public class List
    {
        public class Query : IRequest<List<Movie>> 
        {
        }

        public class Handler : IRequestHandler<Query, List<Movie>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Movie>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Movies.ToListAsync();
            }
        }
    }
}
