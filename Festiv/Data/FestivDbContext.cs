using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Festiv.Models;
using Microsoft.Extensions.Hosting;
using System;


namespace Festiv.Data;

public class FestivDbContext: IdentityDbContext<User>
{
    public DbSet<Event> Events { get; set; }
    public DbSet<User> UserList {get; set;}

            public FestivDbContext(DbContextOptions<FestivDbContext> options) : base(options)
        {
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Event>();
        //     base.OnModelCreating(modelBuilder);
        // }
}

