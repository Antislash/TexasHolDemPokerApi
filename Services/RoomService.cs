using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokerApi.Data;
using PokerApi.Dtos;
using PokerApi.Models;
using TexasHolDemPokerApi.Services.Interface;

namespace PokerApi.Services;

public class RoomService(AppDbContext context, IMapper mapper) : IRoomService
{
    public async Task<object?> Create(RoomDto roomDto, string? email = null)
    {
        var room = mapper.Map<Room>(roomDto);
        context.Room.Add(room);
        await context.SaveChangesAsync();

        if (email is null)
            return mapper.Map<RoomDto>(room);

        var player = await context.Player
            .Include(p => p.Login)
            .FirstOrDefaultAsync(p => p.Login != null && p.Login.Email == email);

        if (player is null)
            return null;

        context.RoomPlayer.Add(new RoomPlayer { RoomId = room.Id, PlayerId = player.Id });
        await context.SaveChangesAsync();

        return new RoomPlayerDto
        {
            Room = mapper.Map<RoomDto>(room),
            Players = [mapper.Map<PlayerDto>(player)]
        };
    }

    public async Task<bool> Delete(int id, bool physicalDelete = true)
    {
        Room? roomToDelete = await context.Room.FindAsync(id);
        if (roomToDelete is null) return false;

        if(physicalDelete)
            context.Room.Remove(roomToDelete);
        else
        {
            roomToDelete.Status = RoomStatus.Deleted;
        }

        await context.SaveChangesAsync();
        return true;
    }

    public Task<List<RoomDto>> GetAll()
    {
        //return context.Room.Where(s => s.Status != RoomStatus.Deleted).Select(e => mapper.Map<RoomDto>(e)).ToListAsync();
        return context.Room.Select(e => mapper.Map<RoomDto>(e)).ToListAsync();

    }

    public async Task<RoomDto> GetById(int id)
    {
        RoomDto? room = mapper.Map<RoomDto>(await context.Room.FindAsync(id));

        return room;
    }
}