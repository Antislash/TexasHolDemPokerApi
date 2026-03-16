namespace PokerApi.Models;

public record Room
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public RoomStatus Status { get; set; }

}

public enum RoomStatus
{
    Draft,
    Playing,
    Deleted
}