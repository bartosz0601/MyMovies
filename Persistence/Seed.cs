using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Movies.Any()) return;
            var movies = new List<Movie>
            {
                new Movie{
                    Title = "Harry Potter i Insygnia Śmierci: Część II",
                    Director = "David Yates",
                    Year = 2011,
                    Rate = 8.1f
                },
                new Movie{
                    Title = "Barry Lyndon",
                    Director = "Stanley Kubrick",
                    Year = 1975,
                    Rate = 8.0f
                }
            };
            await context.Movies.AddRangeAsync(movies);
            await context.SaveChangesAsync();
        }
    }
}
