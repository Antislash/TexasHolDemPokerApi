using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerApi.Dtos;
using TexasHolDemPokerApi.Services.Interface;

namespace PokerApi.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class RoomController(IRoomService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<RoomDto>> GetById(int id)
    {
        RoomDto room = await service.GetById(id);
        return room is null ? NotFound("No room with the given id was found") : Ok(room);
    }

    [HttpGet]
    public async Task<ActionResult<List<RoomDto>>> GetAll()
    {
        var allRooms = await service.GetAll();
        return Ok(allRooms);
    }

    [HttpPost]
    public async Task<ActionResult> Create(RoomDto room, [FromQuery] string? email = null)
    {
        var result = await service.Create(room, email);

        if (result is null)
            return NotFound($"No player found with email '{email}'");

        return CreatedAtAction(nameof(GetAll), result);
    }

    [HttpDelete("{id}/{physicalDelete}")]
    public async Task<ActionResult> Delete(int id, bool physicalDelete = true)
    {
        var deleted = await service.Delete(id, physicalDelete);
        return deleted ? NoContent() : NotFound("No room with the given id was found");
    }
}