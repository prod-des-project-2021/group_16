using ChessWeb.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChessWebClient.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly HttpClient _httpClient;

        public PlayerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<DetailPlayerDTO> GetDetailPlayer(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DetailPlayerDTO>($"/players/{id}");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<List<AllPlayersDTO>> GetPlayersOverview()
        {
            return await _httpClient.GetFromJsonAsync<List<AllPlayersDTO>>("/players");
        }
    }
}
