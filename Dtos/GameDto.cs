using PokerApi.Models;

namespace PokerApi.Dtos;

public record GameDto
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public int? DealerPlayerId { get; set; }
    public int? CurrentPlayerId { get; set; }
    public decimal SmallBlindAmount { get; set; }
    public decimal BigBlindAmount { get; set; }
    public GameStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<GamePlayerDto> Players { get; set; } = [];
}
