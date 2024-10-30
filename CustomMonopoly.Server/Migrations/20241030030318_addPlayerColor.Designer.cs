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
    [Migration("20241030030318_addPlayerColor")]
    partial class addPlayerColor
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
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

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
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
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
                        .HasMaxLength(34)
                        .HasColumnType("nvarchar(34)");

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

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CurrentPostion")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<bool>("IsPlayersTurn")
                        .HasColumnType("bit");

                    b.Property<int>("TurnOrder")
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
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

                    b.Property<int>("RewardCash")
                        .HasColumnType("int");

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

                    b.Property<int>("TurnsInJail")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("JailSquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.PropertySquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.BoardSquare");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MorgageValue")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("PropertySquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.TaxSquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.BoardSquare");

                    b.Property<int>("TaxCost")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("TaxSquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.BuildablePropertySquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.PropertySquare");

                    b.Property<int>("HouseHotelCost")
                        .HasColumnType("int");

                    b.Property<int>("RentFourHouse")
                        .HasColumnType("int");

                    b.Property<int>("RentHotel")
                        .HasColumnType("int");

                    b.Property<int>("RentNoHouse")
                        .HasColumnType("int");

                    b.Property<int>("RentOneHouse")
                        .HasColumnType("int");

                    b.Property<int>("RentThreeHouse")
                        .HasColumnType("int");

                    b.Property<int>("RentTwoHouse")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("BuildablePropertySquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.RailRoadSquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.PropertySquare");

                    b.HasDiscriminator().HasValue("RailroadSquare");
                });

            modelBuilder.Entity("CustomMonopoly.Server.Models.BoardSquares.UtilitySquare", b =>
                {
                    b.HasBaseType("CustomMonopoly.Server.Models.BoardSquares.PropertySquare");

                    b.Property<int>("BaseRent")
                        .HasColumnType("int");

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CustomMonopoly.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CustomMonopoly.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CustomMonopoly.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CustomMonopoly.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
