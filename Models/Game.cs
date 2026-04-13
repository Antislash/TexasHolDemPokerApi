namespace PokerApi.Models;

public class Game
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;
    public int? DealerPlayerId { get; set; }
    public int? CurrentPlayerId { get; set; }
    public decimal SmallBlindAmount { get; set; }
    public decimal BigBlindAmount { get; set; }
    public GameStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<GamePlayer> GamePlayers { get; set; } = [];
}

public enum GameStatus
{
    Waiting,
    Playing,
    Finished
}
