using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VinylLibrarian.Services.OAuth
{
    public class DiscogsOAuthKeys
    { 
        public string ConsumerKey { get; set; } = string.Empty;
        public string ConsumerSecret { get; set; } = string.Empty;

        private readonly HttpClient _httpClient;

        public DiscogsOAuthKeys(IOptions<DiscogsOAuthKeys> oauthKeys, HttpClient httpClient)
        {
            ConsumerKey = oauthKeys.Value.ConsumerKey;
            ConsumerSecret = oauthKeys.Value.ConsumerSecret;
            _httpClient = httpClient;
        }
        
        public DiscogsOAuthKeys() {}

        public async Task<string> GetAccessTokenAsync()
        {
            var requestTokenUrl = "https://api.discogs.com/oauth/request_token";
            var authHeader = $"OAuth oauth_consumer_key=\"{ConsumerKey}\", oauth_nonce=\"{Guid.NewGuid()}\", oauth_signature=\"{ConsumerSecret}&\", oauth_signature_method=\"PLAINTEXT\", oauth_timestamp=\"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}\", oauth_version=\"1.0\"";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);

            var response = await _httpClient.PostAsync(requestTokenUrl, new StringContent(""));

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Request Token Response: " + responseBody);
                
                // Extract request token and return it
                var queryParams = HttpUtility.ParseQueryString(responseBody);
                var requestToken = queryParams["oauth_token"];
                var requestTokenSecret = queryParams["oauth_token_secret"];

                Console.WriteLine($"Authorize at: https://www.discogs.com/oauth/authorize?oauth_token={requestToken}");
                
                return requestToken;
            }
            else
            {
                Console.WriteLine("Failed to get request token: " + response.StatusCode);
                return null;
            }
        }
    }
}