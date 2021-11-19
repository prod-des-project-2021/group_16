using ChessWeb.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessWeb.DAL.Repositories
{
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
    {
        public PlayerRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
