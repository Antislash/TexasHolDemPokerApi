using System.ComponentModel.DataAnnotations;

namespace TexasHolDemPokerApi.Responses;

public class LoginResponse
{
    public string? Pseudo { get; set; }

    [Required]
    public string Email { get; init; }

    public string Token { get; set; }

}
