using ChessWeb.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebAPI
{
    public interface IUnitOfWork
    {
        public IGameRepository Game { get; }
        public IPlayerRepository Player { get; }
        void Complete();
        void Dispose();
    }
}
