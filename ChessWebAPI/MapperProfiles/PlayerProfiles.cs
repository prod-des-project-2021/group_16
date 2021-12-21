using AutoMapper;
using ChessWeb.DAL.Models;
using ChessWeb.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebAPI.MapperProfiles
{
    public class PlayerProfiles : Profile
    {
        public PlayerProfiles()
        {
            CreateMap<Player, DetailPlayerDTO>()
                .ForMember(dst => dst.PlayerId, opt => opt.MapFrom(map => map.Id))
                .ForMember(dst => dst.EmailAddress, opt => opt.MapFrom(map => map.Email))
                .ForMember(dst => dst.Games, opt => opt.MapFrom(map => map.FirstPlayerGames.Concat(map.SecondPlayerGames)));
            
            CreateMap<Player, AllPlayersDTO>()
                .ForMember(dst => dst.PlayerId, opt => opt.MapFrom(map => map.Id));

        }
    }
}
