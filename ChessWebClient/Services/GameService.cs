using ChessWeb.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebClient.Services
{
    public class GameService : IGameService
    {
        public Guid CreateGame(CreateGameDTO game)
        {
            throw new NotImplementedException();
        }

        public DetailGameDTO GetDetailGame(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AllGamesDTO> GetGamesOverview()
        {
            throw new NotImplementedException();
        }
    }
}
