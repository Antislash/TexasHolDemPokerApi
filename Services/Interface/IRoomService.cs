using PokerApi.Dtos;

namespace TexasHolDemPokerApi.Services.Interface;

public interface IRoomService
{
    Task<RoomDto> GetById(int id);

    Task<List<RoomPlayerDto>> GetAll();

    Task<RoomPlayerDto> Create(RoomDto room, string? email = null);

    Task<bool> Delete(int id, bool physicalDelete = true);
}