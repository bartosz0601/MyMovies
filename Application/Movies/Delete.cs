using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movies
{
    public class Delete
    {
        public class Command : IRequest<bool>
        {
            public Guid Id;
        }

        public class Handler : IRequestHandler<Command, bool>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var movie = await _context.Movies.FindAsync(request.Id);
                
                if (movie == null) return false;
                
                _context.Movies.Remove(movie);

                return await _context.SaveChangesAsync() > 0;           
            }
        }
    }
}
