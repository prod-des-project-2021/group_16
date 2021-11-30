using System;
using System.Collections.Generic;
using System.Text;

namespace ChessWeb.Shared.DTOs
{
    public class SignUpResultDTO
    {
        public Guid Id { get; set; }
        public bool IsSuccessfulyRegistered { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
