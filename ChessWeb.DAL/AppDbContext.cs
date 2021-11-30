using ChessWeb.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ChessWeb.DAL
{
    public class AppDbContext : IdentityDbContext<Player, UserRole, Guid>
    {
        //public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>()
                .HasOne(prop => prop.FirstPlayer)
                .WithMany(prop => prop.FirstPlayerGames)
                .HasForeignKey(prop => prop.FirstPlayerId);

            modelBuilder.Entity<Game>()
                .HasOne(prop => prop.SecondPlayer)
                .WithMany(prop => prop.SecondPlayerGames)
                .HasForeignKey(prop => prop.SecondPlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Game>()
                .Property(prop => prop.Date)
                .HasColumnType("date")
                .HasDefaultValueSql("getdate()");

        }
    }
}
