using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokerApi.Data;
using PokerApi.Dtos;
using PokerApi.Models;
using TexasHolDemPokerApi.Services.Interface;

namespace PokerApi.Services;

public class RoomPlayerService(AppDbContext context, IMapper mapper) : IRoomPlayerService
{
    public async Task<List<RoomPlayerDto>> GetAll()
    {
        var entries = await context.RoomPlayer
            .Include(rp => rp.Room)
            .Include(rp => rp.Player)
            .ToListAsync();

        return entries
            .GroupBy(rp => rp.Room!)
            .Select(g => new RoomPlayerDto
            {
                Room = mapper.Map<RoomDto>(g.Key),
                Players = g.Select(rp => mapper.Map<PlayerDto>(rp.Player)).ToList()
            })
            .ToList();
    }

    public async Task<RoomPlayerDto?> GetById(int roomId)
    {
        var entries = await context.RoomPlayer
            .Include(rp => rp.Room)
            .Include(rp => rp.Player)
            .Where(rp => rp.RoomId == roomId)
            .ToListAsync();

        if (entries.Count == 0) return null;

        return new RoomPlayerDto
        {
            Room = mapper.Map<RoomDto>(entries.First().Room),
            Players = entries.Select(rp => mapper.Map<PlayerDto>(rp.Player)).ToList()
        };
    }

    public async Task<RoomPlayerDto?> Create(int roomId, int playerId)
    {
        var room = await context.Room.FindAsync(roomId);
        var player = await context.Player.FindAsync(playerId);

        if (room is null || player is null) return null;

        context.RoomPlayer.Add(new RoomPlayer { RoomId = roomId, PlayerId = playerId });
        await context.SaveChangesAsync();

        return await GetById(roomId);
    }

    public async Task<RoomPlayerDto?> Update(int roomId, int playerId)
    {
        var entry = await context.RoomPlayer.FindAsync(roomId, playerId);
        if (entry is null) return null;

        return await GetById(roomId);
    }

    public async Task<bool> Delete(int roomId, int playerId)
    {
        var roomPlayer = await context.RoomPlayer.FindAsync(roomId, playerId);
        if (roomPlayer is null) return false;

        context.RoomPlayer.Remove(roomPlayer);
        await context.SaveChangesAsync();
        return true;
    }
}
