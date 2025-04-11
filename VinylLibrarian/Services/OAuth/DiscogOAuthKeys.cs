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
        public string AccessToken { get; set; }

        private readonly HttpClient _httpClient;

        public DiscogsOAuthKeys(IOptions<DiscogsOAuthKeys> oauthKeys, HttpClient httpClient)
        {
            ConsumerKey = oauthKeys.Value.ConsumerKey;
            ConsumerSecret = oauthKeys.Value.ConsumerSecret;
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("VinylLibrarian/1.0");
        }
        
        public DiscogsOAuthKeys() {}

        public async Task<string> GetAccessTokenAsync()
        {
            var requestTokenUrl = "https://api.discogs.com/oauth/request_token";
            var authHeader = $"OAuth oauth_consumer_key=\"{ConsumerKey}\", " +
                     $"oauth_nonce=\"{Guid.NewGuid()}\", " +
                     $"oauth_signature=\"{ConsumerSecret}&\", " +
                     $"oauth_signature_method=\"PLAINTEXT\", " +
                     $"oauth_timestamp=\"{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}\", " +
                     $"oauth_version=\"1.0\"";



            var request = new HttpRequestMessage(HttpMethod.Post, requestTokenUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", authHeader.Substring(6));
            request.Content = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var queryParams = HttpUtility.ParseQueryString(responseBody);
                var requestToken = queryParams["oauth_token"];

                // Here, save the request token for further use
                AccessToken = requestToken; // This is the access token you need
                return AccessToken;
            }
            else
            {
                // Handle token request failure
                return null;
            }
        }
    }
}