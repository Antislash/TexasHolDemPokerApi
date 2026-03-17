namespace PokerApi.Dtos;

public record PlayerDto
{
    public int Id { get; set; }
    public string? Pseudo { get; set; }
}
