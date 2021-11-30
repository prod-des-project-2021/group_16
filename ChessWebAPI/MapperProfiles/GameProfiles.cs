using AutoMapper;
using ChessWeb.DAL.Models;
using ChessWeb.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebAPI.MapperProfiles
{
    public class GameProfiles : Profile
    {
        public GameProfiles()
        {
            CreateMap<Game, AllGamesDTO>();
            CreateMap<CreateGameDTO, Game>();
            CreateMap<Game, DetailGameDTO>();
        }
    }
}
