using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokerApi.Data;
using PokerApi.Dtos;
using PokerApi.Models;
using TexasHolDemPokerApi.Services.Interface;

namespace PokerApi.Services;

public class RoomService(AppDbContext context, IMapper mapper) : IRoomService
{
    public async Task<RoomDto> Create(RoomDto roomDto, string creatorEmail)
    {
        var player = await context.Player
            .Include(p => p.Login)
            .FirstOrDefaultAsync(p => p.Login != null && p.Login.Email == creatorEmail);

        var room = mapper.Map<Room>(roomDto);
        room.CreatedAt = DateTime.UtcNow;
        context.Room.Add(room);

        if (player is not null)
        {
            context.RoomPlayer.Add(new RoomPlayer { RoomId = room.Id, PlayerId = player.Id });
        }

        await context.SaveChangesAsync();

        return mapper.Map<RoomDto>(room);
    }

    public async Task<bool> Delete(int id, bool physicalDelete = true)
    {
        Room? roomToDelete = await context.Room.FindAsync(id);
        if (roomToDelete is null) return false;

        if (physicalDelete)
            context.Room.Remove(roomToDelete);
        else
            roomToDelete.Status = RoomStatus.Deleted;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<RoomDto>> GetAll()
    {
        var rooms = await context.Room.ToListAsync();
        return [.. rooms.Select(mapper.Map<RoomDto>)];
    }

    public async Task<RoomDto?> UpdateStatus(int id, RoomStatus status)
    {
        var room = await context.Room.FindAsync(id);
        if (room is null) return null;

        room.Status = status;
        await context.SaveChangesAsync();

        return mapper.Map<RoomDto>(room);
    }

    public async Task<RoomDto> GetById(int id)
    {
        RoomDto? room = mapper.Map<RoomDto>(await context.Room.FindAsync(id));
        return room;
    }

    // Room players

    public async Task<RoomPlayerDto?> GetRoomWithPlayers(int roomId)
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

    public async Task<RoomPlayerDto?> JoinRoom(int roomId, int playerId)
    {
        var room = await context.Room.FindAsync(roomId);
        var player = await context.Player.FindAsync(playerId);

        if (room is null || player is null) return null;
        if (room.Status != RoomStatus.Draft) return null;

        if (!await context.RoomPlayer.AnyAsync(rp => rp.RoomId == roomId && rp.PlayerId == playerId))
        {
            context.RoomPlayer.Add(new RoomPlayer { RoomId = roomId, PlayerId = playerId });
            await context.SaveChangesAsync();
        }

        return await GetRoomWithPlayers(roomId);
    }

    public async Task<bool> LeaveRoom(int roomId, int playerId)
    {
        var roomPlayer = await context.RoomPlayer.FindAsync(roomId, playerId);
        if (roomPlayer is null) return false;

        context.RoomPlayer.Remove(roomPlayer);
        await context.SaveChangesAsync();
        return true;
    }
}
