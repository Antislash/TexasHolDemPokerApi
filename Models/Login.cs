using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TexasHolDemPokerApi.Models;

public class Login
{
    public int Id { get; set; }

    public string? Pseudo { get; set;  }

    [Required]
    public string Email { get; init; }

    [Required]
    public string? PassWord { get; set; }
}
