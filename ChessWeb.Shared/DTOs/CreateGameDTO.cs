using System;
using System.ComponentModel.DataAnnotations;

namespace ChessWeb.Shared.DTOs
{
    public class CreateGameDTO
    {
        [Required(ErrorMessage = "Duration field is required")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "Moves field is required")]
        public string Moves { get; set; }
        public Guid? FirstPlayerId { get; set; }
        public Guid? SecondPlayerId { get; set; }
        public Guid? WhitePiecesPlayer { get; set; }
        public Guid? Winner { get; set; }
    }
}
