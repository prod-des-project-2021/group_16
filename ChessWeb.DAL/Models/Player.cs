using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ChessWeb.Shared.CustomValidations;
using Microsoft.AspNetCore.Identity;

namespace ChessWeb.DAL.Models
{
    public class Player : IdentityUser<Guid>
    {
        public List<Game> FirstPlayerGames = new List<Game>();
        public List<Game> SecondPlayerGames = new List<Game>();
    }
}
