using Microsoft.EntityFrameworkCore;

namespace PokerApi.Models;

[PrimaryKey(nameof(Room), nameof(Player))]
public record RoomPlayer
{
    public Room? Room {get; set;}
    public Player? Player {get; set;}
}