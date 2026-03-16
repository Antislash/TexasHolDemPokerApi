using TexasHolDemPokerApi.Dtos;

namespace TexasHolDemPokerApi.Services.Interface;

public interface ILoginService
{
    Task<LoginDto> RegisterLogin(LoginDto login);

    Task<LoginDto> ConnectLogin(LoginDto login);
}
