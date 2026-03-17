using TexasHolDemPokerApi.Models;

namespace PokerApi.Models;

public record Player
{
    public int Id { get; set; }
    public string? Pseudo { get; set; }
    public Login? Login { get; set; }
    public ICollection<RoomPlayer> RoomPlayers { get; set; } = [];
}
