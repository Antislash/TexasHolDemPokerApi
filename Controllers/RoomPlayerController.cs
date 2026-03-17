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
    [HttpGet]
    public async Task<ActionResult<List<RoomPlayerDto>>> GetAll()
    {
        var roomPlayers = await service.GetAll();
        return Ok(roomPlayers);
    }

    [HttpGet("{roomId}/{playerId}")]
    public async Task<ActionResult<RoomPlayerDto>> GetById(int roomId, int playerId)
    {
        var roomPlayer = await service.GetById(roomId, playerId);
        return roomPlayer is null ? NotFound("No entry found for the given room and player") : Ok(roomPlayer);
    }

    [HttpPost]
    public async Task<ActionResult<RoomPlayerDto>> Create(RoomPlayerDto roomPlayerDto)
    {
        var created = await service.Create(roomPlayerDto);
        return CreatedAtAction(nameof(GetById), new { roomId = created.RoomId, playerId = created.PlayerId }, created);
    }

    [HttpPatch("{roomId}/{playerId}")]
    public async Task<ActionResult<RoomPlayerDto>> Update(int roomId, int playerId, RoomPlayerDto roomPlayerDto)
    {
        var updated = await service.Update(roomId, playerId, roomPlayerDto);
        return updated is null ? NotFound("No entry found for the given room and player") : Ok(updated);
    }

    [HttpDelete("{roomId}/{playerId}")]
    public async Task<ActionResult> Delete(int roomId, int playerId)
    {
        var deleted = await service.Delete(roomId, playerId);
        return deleted ? NoContent() : NotFound("No entry found for the given room and player");
    }
}
