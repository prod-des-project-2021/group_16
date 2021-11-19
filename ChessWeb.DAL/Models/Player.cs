using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ChessWeb.Shared.CustomValidations;

namespace ChessWeb.DAL.Models
{
    public class Player
    {
        [Key]
        public Guid PlayerId { get; set; } 
        
        [Required(ErrorMessage = "Email address field is required")]
        [EmailAddress(ErrorMessage = "Invalid email address was given")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Username field is required")]
        [MaxLength(30, ErrorMessage = "Maximum length of username is 30 characters")]
        [MinLength(2, ErrorMessage = "Minimum length of username is 2 characters")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; }

        public List<Game> FirstPlayerGames = new List<Game>();
        public List<Game> SecondPlayerGames = new List<Game>();

    }
}
