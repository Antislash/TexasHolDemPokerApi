using PokerApi.Dtos;

namespace TexasHolDemPokerApi.Services.Interface;

public interface IPlayerService
{
    Task<PlayerDto?> GetOrCreateByEmail(string email);
}
