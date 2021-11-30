using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWeb.Shared.DTOs
{
    public class SignInDTO
    {
        [Required(ErrorMessage = "Username field is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
