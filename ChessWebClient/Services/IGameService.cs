using ChessWeb.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebClient.Services
{
    public interface IGameService
    {
        Task <List<AllGamesDTO>> GetGamesOverview();
        Task<DetailGameDTO> GetDetailGame(Guid id);
        Task<Guid?> CreateGame(CreateGameDTO game);
        Task DeleteGame(Guid id);

    }
}
