using AutoMapper;
using ChessWeb.DAL.Models;
using ChessWebAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebAPI.MapperProfiles
{
    public class SignUpProfile : Profile
    {
        public SignUpProfile()
        {
            CreateMap<SignUpDTO, Player>();
        }
    }
}
