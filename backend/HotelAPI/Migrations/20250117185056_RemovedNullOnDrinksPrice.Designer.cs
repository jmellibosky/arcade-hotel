﻿// <auto-generated />
using System;
using HotelAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelAPI.Migrations
{
    [DbContext(typeof(ArcadeHotelContext))]
    [Migration("20250117185056_RemovedNullOnDrinksPrice")]
    partial class RemovedNullOnDrinksPrice
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotelAPI.Models.Arcade", b =>
                {
                    b.Property<int>("ArcadeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArcadeId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime");

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("ArcadeId");

                    b.HasIndex("GameId");

                    b.ToTable("Arcades");
                });

            modelBuilder.Entity("HotelAPI.Models.Drink", b =>
                {
                    b.Property<int>("DrinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DrinkId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("DrinkId");

                    b.ToTable("Drinks");
                });

            modelBuilder.Entity("HotelAPI.Models.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("GameId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("HotelAPI.Models.Machine", b =>
                {
                    b.Property<int>("MachinesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MachinesId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("MachinesId");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("HotelAPI.Models.Movement", b =>
                {
                    b.Property<int>("MovementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovementId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<int?>("DrinkId")
                        .HasColumnType("int");

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<int?>("LastResetId")
                        .HasColumnType("int");

                    b.Property<int>("MovementTypeId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("MovementId");

                    b.HasIndex("DrinkId");

                    b.HasIndex("GameId");

                    b.HasIndex("LastResetId");

                    b.HasIndex("MovementTypeId");

                    b.ToTable("Movements");
                });

            modelBuilder.Entity("HotelAPI.Models.MovementType", b =>
                {
                    b.Property<int>("MovementTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovementTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("MovementTypeId");

                    b.ToTable("MovementTypes");
                });

            modelBuilder.Entity("HotelAPI.Models.Reset", b =>
                {
                    b.Property<int>("ResetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResetId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<int>("MovementId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ResetId");

                    b.HasIndex("MovementId");

                    b.HasIndex("UserId");

                    b.ToTable("Resets");
                });

            modelBuilder.Entity("HotelAPI.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<int?>("LastMovementId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Password")
                        .HasColumnType("int");

                    b.HasKey("RoomId");

                    b.HasIndex("LastMovementId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HotelAPI.Models.Slot", b =>
                {
                    b.Property<int>("SlotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SlotId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime");

                    b.Property<int?>("DrinkId")
                        .HasColumnType("int");

                    b.Property<bool>("HasStock")
                        .HasColumnType("bit");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("SlotId");

                    b.HasIndex("DrinkId");

                    b.HasIndex("MachineId");

                    b.ToTable("Slots");
                });

            modelBuilder.Entity("HotelAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("LastLoginAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HotelAPI.Models.Arcade", b =>
                {
                    b.HasOne("HotelAPI.Models.Game", "Game")
                        .WithMany("Arcades")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK_Arcades_Games");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("HotelAPI.Models.Movement", b =>
                {
                    b.HasOne("HotelAPI.Models.Drink", "Drink")
                        .WithMany("Movements")
                        .HasForeignKey("DrinkId")
                        .HasConstraintName("FK_Movements_Drinks");

                    b.HasOne("HotelAPI.Models.Game", "Game")
                        .WithMany("Movements")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK_Movements_Games");

                    b.HasOne("HotelAPI.Models.Reset", "LastReset")
                        .WithMany("Movements")
                        .HasForeignKey("LastResetId")
                        .HasConstraintName("FK_Movements_Resets");

                    b.HasOne("HotelAPI.Models.MovementType", "MovementType")
                        .WithMany("Movements")
                        .HasForeignKey("MovementTypeId")
                        .IsRequired()
                        .HasConstraintName("FK_Movements_MovementTypes");

                    b.Navigation("Drink");

                    b.Navigation("Game");

                    b.Navigation("LastReset");

                    b.Navigation("MovementType");
                });

            modelBuilder.Entity("HotelAPI.Models.Reset", b =>
                {
                    b.HasOne("HotelAPI.Models.Movement", "Movement")
                        .WithMany("Resets")
                        .HasForeignKey("MovementId")
                        .IsRequired()
                        .HasConstraintName("FK_Resets_Movements");

                    b.HasOne("HotelAPI.Models.User", "User")
                        .WithMany("Resets")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Resets_Users");

                    b.Navigation("Movement");

                    b.Navigation("User");
                });

            modelBuilder.Entity("HotelAPI.Models.Room", b =>
                {
                    b.HasOne("HotelAPI.Models.Movement", "LastMovement")
                        .WithMany("Rooms")
                        .HasForeignKey("LastMovementId")
                        .HasConstraintName("FK_Rooms_Movements");

                    b.Navigation("LastMovement");
                });

            modelBuilder.Entity("HotelAPI.Models.Slot", b =>
                {
                    b.HasOne("HotelAPI.Models.Drink", "Drink")
                        .WithMany("Slots")
                        .HasForeignKey("DrinkId")
                        .HasConstraintName("FK_Slots_Drinks");

                    b.HasOne("HotelAPI.Models.Machine", "Machine")
                        .WithMany("Slots")
                        .HasForeignKey("MachineId")
                        .IsRequired()
                        .HasConstraintName("FK_Slots_Machines");

                    b.Navigation("Drink");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("HotelAPI.Models.Drink", b =>
                {
                    b.Navigation("Movements");

                    b.Navigation("Slots");
                });

            modelBuilder.Entity("HotelAPI.Models.Game", b =>
                {
                    b.Navigation("Arcades");

                    b.Navigation("Movements");
                });

            modelBuilder.Entity("HotelAPI.Models.Machine", b =>
                {
                    b.Navigation("Slots");
                });

            modelBuilder.Entity("HotelAPI.Models.Movement", b =>
                {
                    b.Navigation("Resets");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("HotelAPI.Models.MovementType", b =>
                {
                    b.Navigation("Movements");
                });

            modelBuilder.Entity("HotelAPI.Models.Reset", b =>
                {
                    b.Navigation("Movements");
                });

            modelBuilder.Entity("HotelAPI.Models.User", b =>
                {
                    b.Navigation("Resets");
                });
#pragma warning restore 612, 618
        }
    }
}
