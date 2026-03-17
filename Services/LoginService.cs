using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PokerApi.Data;
using TexasHolDemPokerApi.Dtos;
using TexasHolDemPokerApi.Models;
using TexasHolDemPokerApi.Services.Interface;

namespace TexasHolDemPokerApi.Services;
public class LoginService(AppDbContext context, IMapper mapper) : ILoginService
{
    public async Task<LoginDto> Connect(LoginDto loginDto)
    {
        try
        {
            var LoginExist = await (context.Login.
                Where(
                c => (c.Email.Equals(loginDto.Email) == true)
                && (c.PassWord.Equals(loginDto.PassWord) == true)).FirstOrDefaultAsync());

            var loginResponse = mapper.Map<LoginDto>(LoginExist);

            return loginResponse;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<LoginDto> Register(LoginDto loginDto)
    {
        //Convert dto to model
        var login = mapper.Map<Login>(loginDto);

        //Verify if that login doesn't exist in database
        if (await context.Login.Where(c => c.Email.Equals(login.Email) == true).FirstOrDefaultAsync() != null)
        {
            return loginDto;
        }

        //Add model to database
        context.Login.Add(login);

        try
        {
            await context.SaveChangesAsync();
            return loginDto;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
