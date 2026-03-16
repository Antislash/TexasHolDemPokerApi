using PokerApi.Models;

namespace PokerApi.Dtos;

public record RoomDto
{
    public string? Name { get; set; }
    public RoomStatus Status { get; set; }

}