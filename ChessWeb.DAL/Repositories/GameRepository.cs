using ChessWeb.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessWeb.DAL.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
