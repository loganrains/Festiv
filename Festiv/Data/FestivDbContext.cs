using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Festiv.Models;


namespace Festiv.Data;

public class FestivDbContext: IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<Event> Events { get; set; }

            public FestivDbContext(DbContextOptions<FestivDbContext> options) : base(options)
        {
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Event>();
        //     base.OnModelCreating(modelBuilder);
        // }
}

