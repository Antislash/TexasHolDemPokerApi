using PokerApi.Dtos;

namespace TexasHolDemPokerApi.Services.Interface;

public interface IRoomPlayerService
{
    Task<List<RoomPlayerDto>> GetAll();
    Task<RoomPlayerDto?> GetById(int roomId);
    Task<RoomPlayerDto?> Create(int roomId, int playerId);
    Task<RoomPlayerDto?> CreateByEmail(int roomId, string email);
    Task<RoomPlayerDto?> Update(int roomId, int playerId);
    Task<bool> Delete(int roomId, int playerId);
}
