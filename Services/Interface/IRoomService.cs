using PokerApi.Dtos;

namespace TexasHolDemPokerApi.Services.Interface;

public interface IRoomService
{
    Task<RoomDto> GetById(int id);

    Task<List<RoomDto>> GetAll();

    Task<RoomDto> Create(RoomDto room);

    Task<bool> Delete(int id, bool physicalDelete = true);
}