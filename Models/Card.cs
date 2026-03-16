namespace PokerApi.Models;

public record Card
{
    public int Id {get; set;}
    public CardColor color;
    public CardValue value;
}

public enum CardColor
{
    Trefle,
    Coeur,
    Pique,
    Carreau
}

public enum CardValue
{
    As,
    two,
    three,
    four,
    five,
    six,
    seven,
    heigth,
    nine,
    ten,
    Valet,
    Dame,
    Roi
}