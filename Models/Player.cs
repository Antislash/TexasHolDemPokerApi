namespace PokerApi.Models;

public record Player
{
    public int Id { get; set; }
    public string? Login { get; set; }
    public string? PassWord {get ; set; }
}