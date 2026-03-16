using Microsoft.AspNetCore.Mvc;
using TexasHolDemPokerApi.Dtos;
using TexasHolDemPokerApi.Responses;
using TexasHolDemPokerApi.Security;
using TexasHolDemPokerApi.Services;
using TexasHolDemPokerApi.Services.Interface;


namespace TexasHolDemPokerApi.Controllers;

[Route("[controller]")]
[ApiController]
public class LoginController(ILoginService _loginService) : ControllerBase
{

    [HttpPost("register")]
    public async Task<ActionResult> RegisterLogin(LoginDto login)
    {
        LoginDto result = await _loginService.RegisterLogin(login);
        TokenGenerator tokenGenerator = new TokenGenerator();

        return result != null ? Ok(new LoginResponse
        {
            Pseudo = result.Pseudo,
            Email = result.Email,
            Token = tokenGenerator.GenerateToken(login.Email)
        }) : Conflict();
    }


    [HttpPost("connect")]
    public async Task<ActionResult> ConnectLogin(LoginDto login)
    {
        var result = await _loginService.ConnectLogin(login);
        if (result is null) return Unauthorized();


        TokenGenerator tokenGenerator = new TokenGenerator();

        Response.Cookies.Append("token", tokenGenerator.GenerateToken(login.Email), new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            //Use SameSiteMode.None if front not in https or not on the same domain as the api
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddHours(24)
        });

        return Ok(new { result.Pseudo, result.Email });
    }

    // Déconnexion — nécessaire car JS ne peut pas supprimer un cookie HttpOnly
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token");
        return NoContent();
    }
}
