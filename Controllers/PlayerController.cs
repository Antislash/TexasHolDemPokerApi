using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerApi.Dtos;
using TexasHolDemPokerApi.Services.Interface;

namespace PokerApi.Controllers;

[Authorize]
[Route("[controller]")]
[ApiController]
public class PlayerController(IPlayerService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<PlayerDto>> GetOrCreate([FromQuery] string email)
    {
        var player = await service.GetOrCreateByEmail(email);
        return player is null
            ? NotFound($"No login found with email '{email}'")
            : Ok(player);
    }
}
