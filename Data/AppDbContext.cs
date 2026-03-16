using Microsoft.EntityFrameworkCore;
using PokerApi.Models;
using TexasHolDemPokerApi.Models;

namespace PokerApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Card> Card => Set<Card>();
    public DbSet<Player> Player => Set<Player>();
    public DbSet<Room> Room => Set<Room>();
    public DbSet<Login> Login => Set<Login>();
    // public DbSet<RoomPlayer> RoomPlayer => Set<RoomPlayer>();
    // public DbSet<CardPlayer> CardPlayer => Set<CardPlayer>();

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Player>()
    //     .HasMany(e => e.Id)
    //     .

    //     base.OnModelCreating(modelBuilder);
    // }
}