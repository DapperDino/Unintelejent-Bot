using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UnintelejentBot.Models;

namespace UnintelejentBot.Clients
{
    public class WoWClient
    {
        private readonly HttpClient httpClient;
        private readonly IConfigurationRoot config;

        private string token;
        private DateTime tokenExpirationDate;

        public WoWClient(IServiceProvider services)
        {
            httpClient = services.GetRequiredService<IHttpClientFactory>().CreateClient();
            config = services.GetRequiredService<IConfigurationRoot>();
        }

        public async Task<CharacterProfile> GetCharacterProfileSummary(string characterName)
        {
            string url = $"/profile/wow/character/argent-dawn/{characterName.ToLower()}";

            string response = await GetAPIRequest(url, "profile-eu");

            return JsonConvert.DeserializeObject<CharacterProfile>(response);
        }

        public async Task<CharacterMedia> GetCharacterMediaSummary(string characterName)
        {
            string url = $"/profile/wow/character/argent-dawn/{characterName.ToLower()}/character-media";

            string response = await GetAPIRequest(url, "profile-eu");

            return JsonConvert.DeserializeObject<CharacterMedia>(response);
        }

        private async Task<string> GetAPIRequest(string url, string requestNamespace)
        {
            if (DateTime.Now > tokenExpirationDate)
            {
                await GetNewAccessToken();
            }

            url = $"https://eu.api.blizzard.com{url}?namespace={requestNamespace}&locale=en_GB&access_token={token}";

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string response = string.Empty;

            try
            {
                response = await httpClient.GetStringAsync(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error completing request: [{ex.Message}]");
            }

            return response;
        }

        private async Task GetNewAccessToken()
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", config["ClientID"]),
                    new KeyValuePair<string, string>("client_secret", config["ClientSecret"])
                });

                var result = await httpClient.PostAsync("https://eu.battle.net/oauth/token", content);
                var json = await result.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<WowTokenResponse>(json);
                token = tokenResponse.AccessToken;
                tokenExpirationDate = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting token: [{ex.Message}]");
            }
        }
    }
}
