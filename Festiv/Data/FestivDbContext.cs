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
    public Role? Role {get; set;}
    public DbSet<PartyDetails> PartyDetails { get; set; }
    public DbSet<GuestRespond> GuestResponds { get; set; }


    public FestivDbContext(DbContextOptions<FestivDbContext> options): base(options)
    {
    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

            modelBuilder.Entity<Party>().HasOne(p => p.Details).WithOne(d => d.Party).HasForeignKey<PartyDetails>(d => d.PartyId);
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

