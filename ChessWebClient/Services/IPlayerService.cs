using ChessWeb.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebClient.Services
{
    public interface IPlayerService
    {
        Task<List<AllPlayersDTO>> GetPlayersOverview();
        Task<DetailPlayerDTO> GetDetailPlayer(Guid id);
    }
}
