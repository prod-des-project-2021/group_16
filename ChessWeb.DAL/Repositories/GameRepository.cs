using ChessWeb.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessWeb.DAL.Repositories
{
    public class GameRepository : RepositoryBase<Game>, IGameRepository
    {
        public GameRepository(AppDbContext dbContext) : base(dbContext) { }

        public override IEnumerable<Game> GetAll()
        {
            return _dbSet.Include(x => x.FirstPlayer).Include(x => x.SecondPlayer);
        }
        public override Game GetById(Guid id)
        {
            return _dbSet.Include(x => x.FirstPlayer).Include(x => x.SecondPlayer)
                .FirstOrDefault(x => x.GameId == id);
        }
    }
}
