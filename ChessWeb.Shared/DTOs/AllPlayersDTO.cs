using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWeb.Shared.DTOs
{
    public class AllPlayersDTO
    {
        public Guid PlayerId { get; set; }
        public string Username { get; set; }
    }
}
