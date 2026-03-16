using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokerApi.Data;
using PokerApi.Dtos;
using PokerApi.Models;
using TexasHolDemPokerApi.Services.Interface;

namespace PokerApi.Services;

public class RoomService(AppDbContext context, IMapper mapper) : IRoomService
{
    public async Task<RoomDto> CreateRoom(RoomDto roomDto)
    {
        //Convert dto to model
        var room = mapper.Map<Room>(roomDto);

        //Add model to database
        context.Room.Add(room);
        await context.SaveChangesAsync();

        return roomDto;
    }

    public async Task<bool> DeleteRoom(int id, bool physicalDelete = true)
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

    public Task<List<RoomDto>> GetAllRooms()
    {
        //return context.Room.Where(s => s.Status != RoomStatus.Deleted).Select(e => mapper.Map<RoomDto>(e)).ToListAsync();
        return context.Room.Select(e => mapper.Map<RoomDto>(e)).ToListAsync();

    }

    public async Task<RoomDto> GetRoomById(int id)
    {
        RoomDto? room = mapper.Map<RoomDto>(await context.Room.FindAsync(id));

        return room;
    }
}