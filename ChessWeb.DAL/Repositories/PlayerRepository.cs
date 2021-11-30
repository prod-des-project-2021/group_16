using ChessWeb.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessWeb.DAL.Repositories
{
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
    {
        public PlayerRepository(AppDbContext dbContext) : base(dbContext) { }

        public override IEnumerable<Player> GetAll()
        {
            return _dbSet.Include(x => x.FirstPlayerGames).Include(x => x.SecondPlayerGames);
        }

        public override Player GetById(Guid id)
        {
            return _dbSet.Include(x => x.FirstPlayerGames).Include(x => x.SecondPlayerGames)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
