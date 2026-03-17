using PokerApi.Dtos;

namespace TexasHolDemPokerApi.Services.Interface;

public interface IRoomPlayerService
{
    Task<List<RoomPlayerDto>> GetAll();
    Task<RoomPlayerDto?> GetById(int roomId, int playerId);
    Task<RoomPlayerDto> Create(RoomPlayerDto roomPlayerDto);
    Task<RoomPlayerDto?> Update(int roomId, int playerId, RoomPlayerDto roomPlayerDto);
    Task<bool> Delete(int roomId, int playerId);
}
