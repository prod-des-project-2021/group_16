using AutoMapper;
using ChessWeb.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWebAPI.Controllers
{
    [Route("/players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PlayerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AllPlayersDTO>> GetPlayersOverview()
        {
            return Ok(_mapper.Map<IEnumerable<AllPlayersDTO>>(_unitOfWork.Player.GetAll()));
        }

        [HttpGet("{id:Guid}")]
        public ActionResult<DetailPlayerDTO> GetDetailPlayer(Guid id)
        {
            var player = _unitOfWork.Player.GetById(id);

            if (player == null)
            { 
                return NotFound();
            }

            return Ok(_mapper.Map<DetailPlayerDTO>(player));


        }
    }
}
