using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWeb.Shared.DTOs
{
    public class DetailGameDTO
    {
        public Guid GameId { get; set; }
        public string Duration { get; set; }
        public string Moves { get; set; }
        public DateTime Date { get; set; }
        public AllPlayersDTO FirstPlayer { get; set; }
        public AllPlayersDTO SecondPlayer { get; set; }
    }
}
