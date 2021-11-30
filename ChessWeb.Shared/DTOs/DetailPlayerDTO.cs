using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWeb.Shared.DTOs
{
    public class DetailPlayerDTO
    {
        public Guid PlayerId { get; set; }
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public List<AllGamesDTO> Games { get; set; }
        
    }
}
