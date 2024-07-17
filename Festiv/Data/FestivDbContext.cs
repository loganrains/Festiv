using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Festiv.Models;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Festiv.Data;


namespace Festiv.Data;

public class FestivDbContext: DbContext
{
    public DbSet<Party> Parties { get; set; }
    public DbSet<User> UserList { get; set; }

    public DbSet<PartyDetails> PartyDetails { get; set; }

    public DbSet<GuestRespond> GuestResponds { get; set; }

    public FestivDbContext(DbContextOptions<FestivDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }

    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
           builder.Property(x=> x.FirstName);
            builder.Property(x=> x.LastName);
        }
    }
}

