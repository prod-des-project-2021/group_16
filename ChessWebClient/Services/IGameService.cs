using ChessWeb.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebClient.Services
{
    public interface IGameService
    {
        public IEnumerable<AllGamesDTO> GetGamesOverview();
        public DetailGameDTO GetDetailGame(Guid id);
        public Guid CreateGame(CreateGameDTO game);

    }
}
