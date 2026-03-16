using PokerApi.Dtos;

namespace TexasHolDemPokerApi.Services.Interface;

public interface IRoomService
{
    Task<RoomDto> GetRoomById(int id);

    Task<List<RoomDto>> GetAllRooms();

    Task<RoomDto> CreateRoom(RoomDto room);

    Task<bool> DeleteRoom(int id, bool physicalDelete = true);
}