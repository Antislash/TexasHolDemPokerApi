namespace PokerApi.Dtos;

public record RoomPlayerDto
{
    public int RoomId { get; set; }
    public int PlayerId { get; set; }
}
