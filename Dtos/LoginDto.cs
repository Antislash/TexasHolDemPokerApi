using System.ComponentModel.DataAnnotations;

namespace TexasHolDemPokerApi.Dtos;

public class LoginDto
{
    public string? Pseudo { get; set; }

    [Required]
    public string Email { get; init; }

    [Required]
    public string? PassWord { get; set; }
}
