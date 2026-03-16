using Microsoft.EntityFrameworkCore;

namespace PokerApi.Models;

[PrimaryKey(nameof(Card), nameof(Player))]
public record CardPlayer
{
    public Card? Card {get; set;}
    public Player? Player {get; set;}
}