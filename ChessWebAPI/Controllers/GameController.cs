using AutoMapper;
using ChessWeb.DAL.Models;
using ChessWeb.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChessWebAPI.Controllers
{
    [Route("/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GameController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AllGamesDTO>> GetGamesOverview()
        {
            //var id = HttpContext.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(_mapper.Map<IEnumerable<AllGamesDTO>>(_unitOfWork.Game.GetAll()));
        }

        [HttpGet("{id:Guid}")]
        public ActionResult<DetailGameDTO> GetDetailGame(Guid id)
        {
            var game = _unitOfWork.Game.GetById(id);

            if (game == null)
            {
                return NotFound();
            }

            var gameToReturn = _mapper.Map<DetailGameDTO>(game);
            gameToReturn.Moves = System.IO.File.ReadAllText(game.PathToMovesFile);

            return Ok(gameToReturn);

        }

        [Authorize]
        [HttpPost]
        public ActionResult<Guid> CreateGame(CreateGameDTO game)
        {
            var newGame = _mapper.Map<Game>(game);
            
            string path = $@".\Moves\{Guid.NewGuid()}.txt";
            newGame.PathToMovesFile = path; 
            
            _unitOfWork.Game.Create(newGame);

            System.IO.File.WriteAllText(path, game.Moves);	
            
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetDetailGame), new { id = newGame.GameId },
                new { id = newGame.GameId});

        }
    }
}
