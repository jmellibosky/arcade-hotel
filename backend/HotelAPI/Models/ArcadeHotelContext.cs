using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Models;

public partial class ArcadeHotelContext : DbContext
{
    public ArcadeHotelContext()
    {
    }

    public ArcadeHotelContext(DbContextOptions<ArcadeHotelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Arcade> Arcades { get; set; }

    public virtual DbSet<Drink> Drinks { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Machine> Machines { get; set; }

    public virtual DbSet<Movement> Movements { get; set; }

    public virtual DbSet<MovementType> MovementTypes { get; set; }

    public virtual DbSet<Reset> Resets { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? "Data Source=192.168.100.155;Initial Catalog=ArcadeHotel;User ID=hotel;Password=SNEPfluffy85!;Trust Server Certificate=True";
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Arcade>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Game).WithMany(p => p.Arcades)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK_Arcades_Games");
        });

        modelBuilder.Entity<Drink>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Machine>(entity =>
        {
            entity.HasKey(e => e.MachinesId);

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Movement>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Drink).WithMany(p => p.Movements)
                .HasForeignKey(d => d.DrinkId)
                .HasConstraintName("FK_Movements_Drinks");

            entity.HasOne(d => d.Game).WithMany(p => p.Movements)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK_Movements_Games");

            entity.HasOne(d => d.LastReset).WithMany(p => p.Movements)
                .HasForeignKey(d => d.LastResetId)
                .HasConstraintName("FK_Movements_Resets");

            entity.HasOne(d => d.MovementType).WithMany(p => p.Movements)
                .HasForeignKey(d => d.MovementTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movements_MovementTypes");
        });

        modelBuilder.Entity<MovementType>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reset>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Movement).WithMany(p => p.Resets)
                .HasForeignKey(d => d.MovementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resets_Movements");

            entity.HasOne(d => d.User).WithMany(p => p.Resets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Resets_Users");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.Property(e => e.RoomId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.LastMovement).WithMany(p => p.Rooms)
                .HasForeignKey(d => d.LastMovementId)
                .HasConstraintName("FK_Rooms_Movements");
        });

        modelBuilder.Entity<Slot>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Drink).WithMany(p => p.Slots)
                .HasForeignKey(d => d.DrinkId)
                .HasConstraintName("FK_Slots_Drinks");

            entity.HasOne(d => d.Machine).WithMany(p => p.Slots)
                .HasForeignKey(d => d.MachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Slots_Machines");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.LastLoginAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
