﻿// <auto-generated />
using System;
using CustomMonopoly.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CustomMonopoly.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241023163327_updateSchema")]
    partial class updateSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CustomMonopoly.Server.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.Board", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.BoardBoardSquare", b =>
                {
                    b.Property<int>("BoardId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("BoardSquareId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("BoardId", "BoardSquareId");

                    b.HasIndex("BoardSquareId");

                    b.ToTable("BoardBoardSquares");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.BoardSquare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.HasKey("Id");

                    b.ToTable("BoardSquares");

                    b.HasDiscriminator().HasValue("BoardSquare");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Cards.BoardChanceCard", b =>
                {
                    b.Property<int>("BoardId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("ChanceCardId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.HasKey("BoardId", "ChanceCardId");

                    b.HasIndex("ChanceCardId");

                    b.ToTable("BoardChanceCards");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Cards.BoardCommunityChestCard", b =>
                {
                    b.Property<int>("BoardId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("CommunityChestCardId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.HasKey("BoardId", "CommunityChestCardId");

                    b.HasIndex("CommunityChestCardId");

                    b.ToTable("BoardCommunityChestCards");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Cards.ChanceCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MoneyDifference")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChanceCards");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Cards.CommunityChestCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MoneyDifference")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CommunityChestCards");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BoardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Balance")
                        .HasColumnType("int");

                    b.Property<int>("CurrentPostion")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int?>("TurnsInJail")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.PlayerProperty", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("PropertySquareId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.Property<int?>("HotelCount")
                        .HasColumnType("int");

                    b.Property<int?>("HouseCount")
                        .HasColumnType("int");

                    b.HasKey("PlayerId", "PropertySquareId");

                    b.HasIndex("PropertySquareId");

                    b.ToTable("PlayerProperties");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Settings.GameRailRoadMappingSetting", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<int>("RailRoadMappingSettingId")
                        .HasColumnType("int")
                        .HasColumnOrder(2);

                    b.HasKey("GameId", "RailRoadMappingSettingId");

                    b.HasIndex("RailRoadMappingSettingId");

                    b.ToTable("GameRailRoadMappingSettings");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Settings.RailRoadMappingSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("NumberOfRailRoadsOwned")
                        .HasColumnType("int");

                    b.Property<int>("RentCost")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RailRoadMappingSettings");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.ChanceSquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.BoardSquare");

                    b.HasDiscriminator().HasValue("ChanceSquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.CommunityChestSquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.BoardSquare");

                    b.HasDiscriminator().HasValue("CommunityChestSquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.FreeParkingSquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.BoardSquare");

                    b.Property<int>("CashPrize")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("FreeParkingSquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.GoSquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.BoardSquare");

                    b.HasDiscriminator().HasValue("GoSquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.GoToJailSquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.BoardSquare");

                    b.HasDiscriminator().HasValue("GoToJailSquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.JailSquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.BoardSquare");

                    b.HasDiscriminator().HasValue("JailSquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.PropertySquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.BoardSquare");

                    b.HasDiscriminator().HasValue("PropertySquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.TaxSquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.BoardSquare");

                    b.Property<int>("TaxCost")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("TaxSquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.RailRoadSquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.PropertySquare");

                    b.HasDiscriminator().HasValue("RailroadSquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.UtilitySquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.PropertySquare");

                    b.HasDiscriminator().HasValue("UtilitySquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.BoardBoardSquare", b =>
                {
                    b.HasOne("CustomMonopoly.Server.Models.BoardSquares.Board", "Board")
                        .WithMany("BoardBoardSquares")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CustomMonopoly.Server.Models.BoardSquares.BoardSquare", "BoardSquare")
                        .WithMany("BoardBoardSquares")
                        .HasForeignKey("BoardSquareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("BoardSquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Cards.BoardChanceCard", b =>
                {
                    b.HasOne("CustomMonopoly.Server.Models.BoardSquares.Board", "Board")
                        .WithMany()
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CustomMonopoly.Server.Models.Cards.ChanceCard", "ChanceCard")
                        .WithMany()
                        .HasForeignKey("ChanceCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("ChanceCard");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Cards.BoardCommunityChestCard", b =>
                {
                    b.HasOne("CustomMonopoly.Server.Models.BoardSquares.Board", "Board")
                        .WithMany()
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CustomMonopoly.Server.Models.Cards.CommunityChestCard", "CommunityChestCard")
                        .WithMany()
                        .HasForeignKey("CommunityChestCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("CommunityChestCard");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Game", b =>
                {
                    b.HasOne("CustomMonopoly.Server.Models.BoardSquares.Board", "Board")
                        .WithMany()
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Board");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Player", b =>
                {
                    b.HasOne("CustomMonopoly.Server.Models.Game", "Game")
                        .WithMany("Players")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CustomMonopoly.Server.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.PlayerProperty", b =>
                {
                    b.HasOne("CustomMonopoly.Server.Models.Player", "Player")
                        .WithMany("OwnedProperties")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CustomMonopoly.Server.Models.BoardSquares.PropertySquare", "PropertySquare")
                        .WithMany("PlayerProperties")
                        .HasForeignKey("PropertySquareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("PropertySquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Settings.GameRailRoadMappingSetting", b =>
                {
                    b.HasOne("CustomMonopoly.Server.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CustomMonopoly.Server.Models.Settings.RailRoadMappingSetting", "RailRoadMappingSetting")
                        .WithMany()
                        .HasForeignKey("RailRoadMappingSettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("RailRoadMappingSetting");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.Board", b =>
                {
                    b.Navigation("BoardBoardSquares");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.BoardSquare", b =>
                {
                    b.Navigation("BoardBoardSquares");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Game", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.Player", b =>
                {
                    b.Navigation("OwnedProperties");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.PropertySquare", b =>
                {
                    b.Navigation("PlayerProperties");
                });
#pragma warning restore 612, 618
        }
    }
}
