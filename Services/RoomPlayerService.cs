using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokerApi.Data;
using PokerApi.Dtos;
using PokerApi.Models;
using TexasHolDemPokerApi.Services.Interface;

namespace PokerApi.Services;

public class RoomPlayerService(AppDbContext context, IMapper mapper) : IRoomPlayerService
{
    public Task<List<RoomPlayerDto>> GetAll()
    {
        return context.RoomPlayer.Select(rp => mapper.Map<RoomPlayerDto>(rp)).ToListAsync();
    }

    public async Task<RoomPlayerDto?> GetById(int roomId, int playerId)
    {
        var roomPlayer = await context.RoomPlayer.FindAsync(roomId, playerId);
        return roomPlayer is null ? null : mapper.Map<RoomPlayerDto>(roomPlayer);
    }

    public async Task<RoomPlayerDto> Create(RoomPlayerDto roomPlayerDto)
    {
        var roomPlayer = mapper.Map<RoomPlayer>(roomPlayerDto);
        context.RoomPlayer.Add(roomPlayer);
        await context.SaveChangesAsync();
        return roomPlayerDto;
    }

    public async Task<RoomPlayerDto?> Update(int roomId, int playerId, RoomPlayerDto roomPlayerDto)
    {
        var roomPlayer = await context.RoomPlayer.FindAsync(roomId, playerId);
        if (roomPlayer is null) return null;

        mapper.Map(roomPlayerDto, roomPlayer);
        await context.SaveChangesAsync();
        return mapper.Map<RoomPlayerDto>(roomPlayer);
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
