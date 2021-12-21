using System;
using System.ComponentModel.DataAnnotations;

namespace ChessWeb.DAL.Models
{
    public class Game
    {
        [Key]
        public Guid GameId { get; set; }
        
        [Required(ErrorMessage = "Duration field is required")]
        public string Duration { get; set; }
        
        [Required(ErrorMessage = "PathToMoves field is required")]
        public string PathToMovesFile { get; set; }
        public DateTime Date { get; set; }
        public Guid? FirstPlayerId { get; set; }
        public Player FirstPlayer { get; set; }
        public Guid? SecondPlayerId { get; set; }
        public Player SecondPlayer { get; set; }
        public Guid? WhitePiecesPlayer { get; set; }
        public Guid? Winner { get; set; }
    }
}
