﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Festiv.Models;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Abstractions;


namespace Festiv.Data;

public class FestivDbContext: IdentityDbContext<User, Role, Guid>  

{
    public DbSet<User> UserList { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Party> Parties { get; set; }
    public DbSet<PartyDetails> PartyDetails { get; set; }
    public DbSet<GuestRespond> GuestResponds { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Gift> Gifts { get; set; }
    public DbSet<GameSignUp> GameSignUp { get; set; }


    public FestivDbContext(DbContextOptions<FestivDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);

        // modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

             modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            // Party and PartyDetail's Relationship
            modelBuilder.Entity<Party>()
                .HasOne(p => p.Details)
                .WithOne(d => d.Party)
                .HasForeignKey<PartyDetails>(d => d.PartyId);

            // Party and Game's Relationship
             modelBuilder.Entity<Party>()
                .HasMany(p => p.Games)
                .WithOne(g => g.Party)
                .HasForeignKey(g => g.PartyId);

            // Party and GuestResponds Relationship
            modelBuilder.Entity<GuestRespond>()
                .HasOne(gr => gr.Party)
                .WithMany(p => p.GuestResponds)
                .HasForeignKey(gr => gr.PartyId);

            // User and GuestResponds Relationshiop
            modelBuilder.Entity<GuestRespond>()
                .HasOne(gr => gr.User)
                .WithMany(u => u.GuestResponds)
                .HasForeignKey(gr => gr.UserId);

            // User and Gift's Relationship
            modelBuilder.Entity<Gift>()
                .HasOne(g => g.User)
                .WithMany(u => u.Gifts)
                .HasForeignKey(g => g.UserId);

            // Gift and Party's Relationship
            modelBuilder.Entity<Gift>()
                .HasOne(g => g.Party)
                .WithMany(p => p.Gifts)
                .HasForeignKey(g => g.PartyId);

            // Game and Team Relationship
            modelBuilder.Entity<Game>()
                .HasMany(g => g.Teams)
                .WithOne(t => t.Game)
                .HasForeignKey(t => t.GameId);

            // User and Game Relationship
            modelBuilder.Entity<Game>()
                .HasMany(g => g.WaitingPlayers)
                .WithMany(u => u.WaitingPlayers)
                .UsingEntity<Dictionary<string, object>>(
                    "GameWaitingPlayer",
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j => j.HasOne<Game>().WithMany().HasForeignKey("GameId"));

            modelBuilder.Entity<Game>()
                .HasMany(g => g.CurrentPlayers)
                .WithMany(u => u.CurrentPlayers)
                .UsingEntity<Dictionary<string, object>>(
                    "GameCurrentPlayer",
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j => j.HasOne<Game>().WithMany().HasForeignKey("GameId"));
            
            // User and Team Relatioships
            modelBuilder.Entity<Team>()
                .HasMany(t =>t.Members)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "TeamMember",
                    j => j.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    j => j.HasOne<Team>().WithMany().HasForeignKey("TeamId")

        );


        var adminRoleId = Guid.NewGuid();
        var userRoleId = Guid.NewGuid();
        var adminId = Guid.NewGuid();

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
            new Role { Id = userRoleId, Name = "User", NormalizedName = "USER" }
        );

        var hasher = new PasswordHasher<User>();
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminId,
                UserName = "admin@festiv.com",
                NormalizedUserName = "ADMIN@FESTIV.COM",
                Email = "admin@festiv.com",
                NormalizedEmail = "ADMIN@FESTIV.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin@123"),
                SecurityStamp = string.Empty,
                FirstName = "Admin",
                LastName = "User",
                UserType = true // assuming true means admin
            }
        );

        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(
            new IdentityUserRole<Guid>
            {
                RoleId = adminRoleId,
                UserId = adminId
            }
        );
    }

    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x=> x.FirstName);
            builder.Property(x=> x.LastName);
            builder.Property(x=> x.Rating);
        }
    }
}

