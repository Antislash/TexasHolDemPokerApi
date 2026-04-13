using PokerApi.Dtos;

namespace TexasHolDemPokerApi.Services.Interface;

public interface IGameService
{
    Task<GameDto?> GetById(int id);
    Task<GameDto?> Create(int roomId, string creatorEmail);
}
