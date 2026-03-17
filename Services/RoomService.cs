using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokerApi.Data;
using PokerApi.Dtos;
using PokerApi.Models;
using TexasHolDemPokerApi.Services.Interface;

namespace PokerApi.Services;

public class RoomService(AppDbContext context, IMapper mapper, IRoomPlayerService roomPlayerService) : IRoomService
{
    public async Task<RoomPlayerDto> Create(RoomDto roomDto, string? email = null)
    {
        var room = mapper.Map<Room>(roomDto);
        context.Room.Add(room);
        await context.SaveChangesAsync();

        if (email is null)
            return new RoomPlayerDto { Room = mapper.Map<RoomDto>(room), Players = [] };

        return await roomPlayerService.CreateByEmail(room.Id, email)
            ?? new RoomPlayerDto { Room = mapper.Map<RoomDto>(room), Players = [] };
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

    public async Task<List<RoomPlayerDto>> GetAll()
    {
        var roomPlayers = await context.RoomPlayer
            .Include(rp => rp.Room)
            .Include(rp => rp.Player)
            .ToListAsync();

        return roomPlayers
            .GroupBy(rp => rp.Room)
            .Select(g => new RoomPlayerDto
            {
                Room = mapper.Map<RoomDto>(g.Key),
                Players = g.Select(rp => mapper.Map<PlayerDto>(rp.Player)).ToList()
            })
            .ToList();
    }

    public async Task<RoomDto> GetById(int id)
    {
        RoomDto? room = mapper.Map<RoomDto>(await context.Room.FindAsync(id));

        return room;
    }
}