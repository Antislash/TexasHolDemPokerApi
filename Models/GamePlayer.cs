using Microsoft.EntityFrameworkCore;

namespace PokerApi.Models;

public class GamePlayer
{
    public int GameId { get; set; }
    public Game Game { get; set; } = null!;

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    [Precision(18, 2)]
    public decimal Stack { get; set; }

    [Precision(18, 2)]
    public decimal CurrentBet { get; set; }

    public bool HasFolded { get; set; }
}
