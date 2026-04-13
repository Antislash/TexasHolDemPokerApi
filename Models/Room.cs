using Microsoft.EntityFrameworkCore;

namespace PokerApi.Models;

public record Room
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int MaxPlayers { get; set; }
    public RoomStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    [Precision(18, 2)]
    public decimal Stack { get; set; } = 500;
    public ICollection<RoomPlayer> RoomPlayers { get; set; } = [];
}

public enum RoomStatus
{
    Draft,
    Playing,
    Finished,
    Deleted
}