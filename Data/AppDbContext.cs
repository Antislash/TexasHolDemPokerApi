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
    // public DbSet<CardPlayer> CardPlayer => Set<CardPlayer>();
    public DbSet<RoomPlayer> RoomPlayer => Set<RoomPlayer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoomPlayer>()
            .HasKey(rp => new { rp.RoomId, rp.PlayerId });

        modelBuilder.Entity<RoomPlayer>()
            .HasOne(rp => rp.Room)
            .WithMany(r => r.RoomPlayers)
            .HasForeignKey(rp => rp.RoomId);

        modelBuilder.Entity<RoomPlayer>()
            .HasOne(rp => rp.Player)
            .WithMany(p => p.RoomPlayers)
            .HasForeignKey(rp => rp.PlayerId);

        modelBuilder.Entity<Room>()
            .Property(r => r.MaxPlayers)
            .HasDefaultValue(8);

        modelBuilder.Entity<Room>()
            .HasOne<Player>()
            .WithMany()
            .HasForeignKey(r => r.DealerPlayerId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Player>()
            .HasOne(p => p.Login)
            .WithMany();

        base.OnModelCreating(modelBuilder);
    }
}