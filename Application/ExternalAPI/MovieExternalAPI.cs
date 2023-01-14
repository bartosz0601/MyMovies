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

        public async Task<List<MovieDTO>> GetMovies()
        {
            var url = "/MyMovies";
            var result = new List<MovieDTO>();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();
                result = JsonSerializer.Deserialize<List<MovieDTO>>(stringResponse,
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
