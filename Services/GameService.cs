using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokerApi.Data;
using PokerApi.Dtos;
using PokerApi.Models;
using TexasHolDemPokerApi.Services.Interface;

namespace PokerApi.Services;

public class GameService(AppDbContext context, IMapper mapper) : IGameService
{
    public async Task<GameDto?> GetById(int id)
    {
        var game = await context.Game
            .Include(g => g.GamePlayers)
            .ThenInclude(gp => gp.Player)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (game is null) return null;
        return ToDto(game);
    }

    public async Task<GameDto?> Create(int roomId, string creatorEmail)
    {
        var room = await context.Room
            .Include(r => r.RoomPlayers)
            .ThenInclude(rp => rp.Player)
            .FirstOrDefaultAsync(r => r.Id == roomId);

        if (room is null || room.Status != RoomStatus.Draft) return null;

        var creator = await context.Player
            .Include(p => p.Login)
            .FirstOrDefaultAsync(p => p.Login != null && p.Login.Email == creatorEmail);

        var game = new Game
        {
            RoomId = roomId,
            DealerPlayerId = creator?.Id,
            CurrentPlayerId = creator?.Id,
            SmallBlindAmount = 25,
            BigBlindAmount = 50,
            Status = GameStatus.Waiting,
            CreatedAt = DateTime.UtcNow,
        };

        context.Game.Add(game);

        foreach (var rp in room.RoomPlayers)
        {
            context.GamePlayer.Add(new GamePlayer
            {
                GameId = game.Id,
                PlayerId = rp.PlayerId,
                Stack = rp.Stack,
            });
        }

        room.Status = RoomStatus.Playing;
        await context.SaveChangesAsync();

        return await GetById(game.Id);
    }

    private GameDto ToDto(Game game) => new()
    {
        Id = game.Id,
        RoomId = game.RoomId,
        DealerPlayerId = game.DealerPlayerId,
        CurrentPlayerId = game.CurrentPlayerId,
        SmallBlindAmount = game.SmallBlindAmount,
        BigBlindAmount = game.BigBlindAmount,
        Status = game.Status,
        CreatedAt = game.CreatedAt,
        Players = game.GamePlayers.Select(gp => new GamePlayerDto
        {
            Player = mapper.Map<PlayerDto>(gp.Player),
            Stack = gp.Stack,
            CurrentBet = gp.CurrentBet,
            HasFolded = gp.HasFolded,
        }).ToList()
    };
}
