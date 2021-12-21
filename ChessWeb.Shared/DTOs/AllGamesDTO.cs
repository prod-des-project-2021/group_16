using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWeb.Shared.DTOs
{
    public class AllGamesDTO
    {
        public Guid GameId { get; set; }
        public string Duration { get; set; }
        public string Date { get; set; }
        public AllPlayersDTO FirstPlayer { get; set; }
        public AllPlayersDTO SecondPlayer { get; set; }
        public Guid? WhitePiecesPlayer { get; set; }
        public Guid? Winner { get; set; }

    }
}
