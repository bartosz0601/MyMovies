using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.ExternalAPI
{
    public class MovieExternalAPI : IMovieExternalAPI
    {
        private readonly static HttpClient client;

        static MovieExternalAPI()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://filmy.programdemo.pl/")
            };
        }

        public async Task<List<MovieExtDTO>> GetMovies(CancellationToken ct)
        {
            var url = "/MyMovies";
            var result = new List<MovieExtDTO>();
            var response = await client.GetAsync(url, ct);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync(ct);
                result = JsonSerializer.Deserialize<List<MovieExtDTO>>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                throw new HttpRequestException(response.ReasonPhrase);
            }
            return result;
        }
    }
}
