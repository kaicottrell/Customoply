﻿using Microsoft.EntityFrameworkCore;
using CustomMonopoly.Server.Models;
using CustomMonopoly.Server.Models.BoardSquares;
using CustomMonopoly.Server.Models.Cards;
using CustomMonopoly.Server.Models.Settings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CustomMonopoly.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Board> Boards { get; set; }

        //Board Squares
        public DbSet<BoardSquare> BoardSquares { get; set; }
        public DbSet<BoardBoardSquare> BoardBoardSquares { get; set; }
        public DbSet<ChanceSquare> ChanceSquares { get; set; }
        public DbSet<CommunityChestSquare> CommunityChestSquares { get; set; }
        public DbSet<FreeParkingSquare> FreeParkingSquares { get; set; }
        public DbSet<GoSquare> GoSquares { get; set; }
        public DbSet<GoToJailSquare> GoToJailSquares { get; set; }
        public DbSet<JailSquare> JailSquares { get; set; }
        public DbSet<PropertySquare> PropertySquares { get; set; }
        public DbSet<RailRoadSquare> RailRoadSquares { get; set; }
        public DbSet<TaxSquare> TaxSquares { get; set; }


        //Cards
        public DbSet<BoardChanceCard> BoardChanceCards { get; set; }
        public DbSet<BoardCommunityChestCard> BoardCommunityChestCards { get; set; }
        public DbSet<CommunityChestCard> CommunityChestCards { get; set; }
        public DbSet<ChanceCard> ChanceCards { get; set; }

        //Settings
        public DbSet<GameRailRoadMappingSetting> GameRailRoadMappingSettings { get; set; }
        public DbSet<RailRoadMappingSetting> RailRoadMappingSettings { get; set; }


        //Games
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerProperty> PlayerProperties { get; set; }
        public DbSet<Game> Games { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the many-to-many relationship
            modelBuilder.Entity<BoardBoardSquare>()
                .HasKey(bbs => new { bbs.BoardId, bbs.BoardSquareId });

            modelBuilder.Entity<BoardChanceCard>()
                .HasKey(bcc => new { bcc.BoardId, bcc.ChanceCardId });
            modelBuilder.Entity<BoardCommunityChestCard>()
                .HasKey(bccc => new { bccc.BoardId, bccc.CommunityChestCardId });

            modelBuilder.Entity<PlayerProperty>()
                .HasKey(pp => new { pp.PlayerId, pp.PropertySquareId });
            modelBuilder.Entity<GameRailRoadMappingSetting>()
                .HasKey(grrms => new { grrms.GameId, grrms.RailRoadMappingSettingId });

            //Configure relationship for player properties having one player (with many player properties)
            //modelBuilder.Entity<PlayerProperty>()
            //   .HasOne(pp => pp.Player)
            //   .WithMany(p => p.OwnedProperties)
            //   .HasForeignKey(pp => pp.PlayerId);
            //Configure relationship for player properties having one property (with many player properties)
            //modelBuilder.Entity<PlayerProperty>()
            //   .HasOne(pp => pp.PropertySquare)
            //   .WithMany(p => p.PlayerProperties)
            //   .HasForeignKey(pp => pp.PropertySquareId);

            //modelBuilder.Entity<BoardBoardSquare>()
            //     .HasOne(bbs => bbs.Board)
            //     .WithMany(b => b.BoardBoardSquares)
            //     .HasForeignKey(bbs => bbs.BoardId);

            //modelBuilder.Entity<BoardBoardSquare>()
            //    .HasOne(bbs => bbs.BoardSquare)
            //    .WithMany(bs => bs.BoardBoardSquares)
            //    .HasForeignKey(bbs => bbs.BoardSquareId);


            //// Configure the discriminator for the BoardSquare hierarchy
            modelBuilder.Entity<BoardSquare>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<BuildablePropertySquare>("BuildablePropertySquare")
                .HasValue<FreeParkingSquare>("FreeParkingSquare")
                .HasValue<GoSquare>("GoSquare")
                .HasValue<GoToJailSquare>("GoToJailSquare")
                .HasValue<JailSquare>("JailSquare")
                .HasValue<CommunityChestSquare>("CommunityChestSquare")
                .HasValue<ChanceSquare>("ChanceSquare")
                .HasValue<PropertySquare>("PropertySquare")
                .HasValue<RailRoadSquare>("RailroadSquare")
                .HasValue<TaxSquare>("TaxSquare")
                .HasValue<UtilitySquare>("UtilitySquare");


            // Additional configurations for other derived types can be added here
        }
    }
}
