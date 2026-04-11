using Microsoft.EntityFrameworkCore;

namespace PokerApi.Models;

public record RoomPlayer
{
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    public bool IsDealer { get; set; }
    
    [Precision(18, 2)]
    public decimal Stack { get; set; }
}
