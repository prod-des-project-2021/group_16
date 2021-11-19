using ChessWeb.DAL;
using ChessWeb.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebAPI
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _dbContext;
        private IGameRepository gameRepository;
        private IPlayerRepository playerRepository;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGameRepository Game 
        {
            get
            {
                if (gameRepository == null)
                {
                    gameRepository = new GameRepository(_dbContext);
                }

                return gameRepository;
            }
        }

        public IPlayerRepository Player
        {
            get
            {
                if (playerRepository == null)
                {
                    playerRepository = new PlayerRepository(_dbContext);
                }

                return playerRepository;
            }
        }
        public void Complete()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
