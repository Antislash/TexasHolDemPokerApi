namespace PokerApi.Dtos;

public record RoomPlayerDto
{
    public RoomDto Room { get; set; } = null!;
    public List<PlayerDto> Players { get; set; } = [];
}
