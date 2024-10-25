using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Favourite> Favorites { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Influencer> Influencers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Influencer>()
                .Property(i => i.BookingPrice)
                .HasColumnType("decimal(18,2)"); 

            modelBuilder.Entity<Proposal>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); 

        }

    }
}