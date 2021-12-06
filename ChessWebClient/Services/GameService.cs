using ChessWeb.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChessWebClient.Services
{
    public class GameService : IGameService
    {
        private readonly HttpClient _httpClient;
        public GameService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AllGamesDTO>> GetGamesOverview()
        {
            return await _httpClient.GetFromJsonAsync<List<AllGamesDTO>>("/games");
        }

        public async Task<string> CreateGame(CreateGameDTO game)
        {
            var result = await _httpClient.PostAsJsonAsync("/games", game);
            return result.Headers.Location.ToString();
            
        }

        public async Task<DetailGameDTO> GetDetailGame(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DetailGameDTO>($"/games/{id}");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
