using System.Net.Http;
using DomainModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using VinylLibrarian.Services.Interfaces;

namespace VinylLibrarian.Services.Classes
{
    public class DiscogServices : IDiscogServices
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DiscogServices> _logger;
        private const string ApiUrl = "https://api.discogs.com/database/search";
        private const string ApiToken = "jlrjbeHlKGDWKazKGZUsyZbvGHHogklbzVVbjxPB";

        public DiscogServices(HttpClient httpClient, ILogger<DiscogServices> logger)
        {
            _httpClient = httpClient;
             _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<List<DiscogRecord>> SearchAlbumsAsync(string query = "Pink Floyd", int limit = 10, int page = 1)
        {
            string encodedQuery = Uri.EscapeDataString(query);
            string url = $"{ApiUrl}?q={encodedQuery}&type=release&token={ApiToken}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            _logger.LogInformation($"Discogs API Response Status: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning($"Discogs API request failed. Status Code: {response.StatusCode}");
                return new List<DiscogRecord>();
            }

            string json = await response.Content.ReadAsStringAsync();
            
            _logger.LogDebug($"Discogs API Response JSON: {json}");

            var data = JsonSerializer.Deserialize<DiscogsResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (data?.Results == null || !data.Results.Any())
            {
                _logger.LogWarning("Discogs API returned no results.");
                return new List<DiscogRecord>();
            }

            return data.Results.ConvertAll(r => new DiscogRecord
            {
                Title = r.Title,
                Artist = r.Artist,
                Genre = r.Genre,
            });
        }
    }
}
