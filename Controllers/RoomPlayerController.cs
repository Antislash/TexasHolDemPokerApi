using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerApi.Dtos;
using TexasHolDemPokerApi.Services.Interface;

namespace PokerApi.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class RoomPlayerController(IRoomPlayerService service) : ControllerBase
{
    [HttpGet("player/{playerId}")]
    public async Task<ActionResult<List<RoomPlayerDto>>> GetRoomsByPlayer(int playerId)
    {
        var rooms = await service.GetRoomsByPlayerId(playerId);
        return Ok(rooms);
    }

    [HttpGet]
    public async Task<ActionResult<List<RoomPlayerDto>>> GetAll()
    {
        var roomPlayers = await service.GetAll();
        return Ok(roomPlayers);
    }

    [HttpGet("{roomId}")]
    public async Task<ActionResult<RoomPlayerDto>> GetById(int roomId)
    {
        var roomPlayer = await service.GetById(roomId);
        return roomPlayer is null ? NotFound("No entry found for the given room") : Ok(roomPlayer);
    }

    [HttpPost("{roomId}/{playerId}")]
    public async Task<ActionResult<RoomPlayerDto>> Create(int roomId, int playerId)
    {
        var created = await service.Create(roomId, playerId);
        return created is null
            ? NotFound("Room or player not found")
            : CreatedAtAction(nameof(GetById), new { roomId }, created);
    }

    [HttpPatch("{roomId}/{playerId}")]
    public async Task<ActionResult<RoomPlayerDto>> Update(int roomId, int playerId)
    {
        var updated = await service.Update(roomId, playerId);
        return updated is null ? NotFound("No entry found for the given room and player") : Ok(updated);
    }

    [HttpDelete("{roomId}/{playerId}")]
    public async Task<ActionResult> Delete(int roomId, int playerId)
    {
        var deleted = await service.Delete(roomId, playerId);
        return deleted ? NoContent() : NotFound("No entry found for the given room and player");
    }
}
