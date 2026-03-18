using PokerApi.Models;

namespace PokerApi.Dtos;

public record RoomDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int MaxPlayers { get; set; }
    public RoomStatus Status { get; set; }
}