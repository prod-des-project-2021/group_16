using System;
using System.Collections.Generic;
using System.Text;

namespace ChessWeb.Shared.DTOs
{
    public class SignInResultDTO
    {
        public bool IsAuthSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}
