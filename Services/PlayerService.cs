using Microsoft.EntityFrameworkCore;
using PokerApi.Data;
using PokerApi.Models;
using TexasHolDemPokerApi.Services.Interface;

namespace PokerApi.Services;

public class PlayerService(AppDbContext context) : IPlayerService
{
    public async Task<Player?> GetOrCreateByEmail(string email)
    {
        var player = await context.Player
            .Include(p => p.Login)
            .FirstOrDefaultAsync(p => p.Login != null && p.Login.Email == email);

        if (player is not null) return player;

        var login = await context.Login.FirstOrDefaultAsync(l => l.Email == email);
        if (login is null) return null;

        player = new Player { Pseudo = login.Pseudo, Login = login };
        context.Player.Add(player);
        await context.SaveChangesAsync();

        return player;
    }
}
