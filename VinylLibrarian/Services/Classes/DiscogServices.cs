using System.Net.Http;
using DomainModel;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using VinylLibrarian.Services.Interfaces;
using VinylLibrarian.Services.OAuth;

namespace VinylLibrarian.Services.Classes
{
    public class DiscogServices : IDiscogServices
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DiscogServices> _logger;
        private readonly DiscogsOAuthKeys _discogsOAuthKeys;
        private const string ApiUrl = "https://api.discogs.com/database/search";

        public DiscogServices(HttpClient httpClient, ILogger<DiscogServices> logger, DiscogsOAuthKeys discogsOAuthKeys)
        {
            _httpClient = httpClient;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _discogsOAuthKeys = discogsOAuthKeys ?? throw new ArgumentNullException(nameof(discogsOAuthKeys));

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("VinylLibrarian/1.0");
        }


        public async Task<List<DiscogRecord>> SearchAlbumsAsync(string query, int limit, int page)
        {
            string encodedQuery = Uri.EscapeDataString(query);

            string url = $"{ApiUrl}?q={encodedQuery}&type=release&per_page={limit}&page={page}&key={_discogsOAuthKeys.ConsumerKey}&secret={_discogsOAuthKeys.ConsumerSecret}";

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
                Artist = r.Artist ?? "Unknown", // If Artist is a string (or flatten Artists list)
                Genre = r.Genre ?? new List<string>(),
                Cover_Image = r.Cover_Image ?? "",
            });
        }
    }
}
