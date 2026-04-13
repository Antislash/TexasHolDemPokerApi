using PokerApi.Dtos;
using PokerApi.Models;

namespace TexasHolDemPokerApi.Services.Interface;

public interface IRoomService
{
    Task<RoomDto> GetById(int id);
    Task<List<RoomDto>> GetAll();
    Task<RoomDto> Create(RoomDto room, string creatorEmail);
    Task<bool> Delete(int id, bool physicalDelete = true);
    Task<RoomDto?> UpdateStatus(int id, RoomStatus status);

    // Room players
    Task<RoomPlayerDto?> GetRoomWithPlayers(int roomId);
    Task<RoomPlayerDto?> JoinRoom(int roomId, int playerId);
    Task<bool> LeaveRoom(int roomId, int playerId);
}
