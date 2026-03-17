using TexasHolDemPokerApi.Dtos;

namespace TexasHolDemPokerApi.Services.Interface;

public interface ILoginService
{
    Task<LoginDto> Register(LoginDto login);

    Task<LoginDto> Connect(LoginDto login);
}
