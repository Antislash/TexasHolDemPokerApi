namespace PokerApi.Models;

public record RoomPlayer
{
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;
}
