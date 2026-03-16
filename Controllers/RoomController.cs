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
    public async Task<ActionResult<RoomDto>> GetRoomById(int id)
    {
        RoomDto room = await service.GetRoomById(id);
        return room is null ? NotFound("No room with the given id was found") : Ok(room);
    }

    [HttpGet]
    public async Task<ActionResult<List<RoomDto>>> GetRooms()
    {
        var allRooms = await service.GetAllRooms();
        return Ok(allRooms);
    }

    [HttpPost]
    public async Task<ActionResult<RoomDto>> CreateRoom(RoomDto room)
    {
        var createdRoom = await service.CreateRoom(room);
        return CreatedAtAction(nameof(GetRooms), createdRoom);
    }

    
    [HttpDelete("{id}/{physicalDelete}")]
    public async Task<ActionResult> DeleteRoom(int id, bool physicalDelete = true)
    {
        var deleted = await service.DeleteRoom(id, physicalDelete);
        return deleted ? NoContent() : NotFound("No room with the given id was found");
    }
}