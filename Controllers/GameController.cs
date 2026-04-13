using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerApi.Dtos;
using TexasHolDemPokerApi.Services.Interface;

namespace PokerApi.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class GameController(IGameService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<GameDto>> GetById(int id)
    {
        var game = await service.GetById(id);
        return game is null ? NotFound("No game with the given id was found") : Ok(game);
    }

    [HttpPost("{roomId}")]
    public async Task<ActionResult<GameDto>> Create(int roomId)
    {
        var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
        var game = await service.Create(roomId, email);
        return game is null
            ? NotFound("Room not found or not in Draft status")
            : CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
    }
}
