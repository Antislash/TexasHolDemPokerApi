namespace PokerApi.Dtos;

public record GamePlayerDto
{
    public PlayerDto Player { get; set; } = null!;
    public decimal Stack { get; set; }
    public decimal CurrentBet { get; set; }
    public bool HasFolded { get; set; }
}
