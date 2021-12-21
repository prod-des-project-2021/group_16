using AutoMapper;
using ChessWeb.DAL.Models;
using ChessWeb.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebAPI.MapperProfiles
{
    public class GameProfiles : Profile
    {
        public GameProfiles()
        {
            CreateMap<Game, AllGamesDTO>()
                .ForMember(dst => dst.Date, opt => opt.MapFrom(game => game.Date.ToString("D", CultureInfo.GetCultureInfo("en-US"))));
            CreateMap<CreateGameDTO, Game>();
            CreateMap<Game, DetailGameDTO>()
                .ForMember(dst => dst.Date, opt => opt.MapFrom(game => game.Date.ToString("D", CultureInfo.GetCultureInfo("en-US"))));
        }
    }
}
