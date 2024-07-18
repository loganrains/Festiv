using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    public DbSet<Party> Parties { get; set; }
    public DbSet<PartyDetails> PartyDetails { get; set; }
    public DbSet<GuestRespond> GuestResponds { get; set; }


    public FestivDbContext(DbContextOptions<FestivDbContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

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

