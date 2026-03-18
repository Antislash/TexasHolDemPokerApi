using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokerApi.Data;
using PokerApi.Dtos;
using PokerApi.Models;
using TexasHolDemPokerApi.Services.Interface;

namespace PokerApi.Services;

public class RoomService(AppDbContext context, IMapper mapper) : IRoomService
{
    public async Task<RoomDto> Create(RoomDto roomDto)
    {
        var room = mapper.Map<Room>(roomDto);
        context.Room.Add(room);
        await context.SaveChangesAsync();

        return mapper.Map<RoomDto>(room);
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

    public async Task<List<RoomDto>> GetAll()
    {
        var rooms = await context.Room.ToListAsync();
        return [.. rooms.Select(mapper.Map<RoomDto>)];
    }

    public async Task<RoomDto> GetById(int id)
    {
        RoomDto? room = mapper.Map<RoomDto>(await context.Room.FindAsync(id));

        return room;
    }
}