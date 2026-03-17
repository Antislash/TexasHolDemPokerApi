using PokerApi.Models;

namespace TexasHolDemPokerApi.Services.Interface;

public interface IPlayerService
{
    Task<Player?> GetOrCreateByEmail(string email);
}
