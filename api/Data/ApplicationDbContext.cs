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

            modelBuilder.Entity<User>()
               .HasOne(u => u.Influencer)
               .WithOne(i => i.User)
               .HasForeignKey<Influencer>(i => i.UserId)
               .OnDelete(DeleteBehavior.Cascade); // Khi xóa User, xóa Influencer

            modelBuilder.Entity<User>()
                .HasOne(u => u.Brand)
                .WithOne(b => b.User)
                .HasForeignKey<Brand>(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Khi xóa User, xóa Brand
            modelBuilder.Entity<User>().HasData(
        new User
        {
            UserId = 1,
            UserName = "hanni_44",
            Email = "ni.trnh59@gmail.com",
            Password = "Abc123#", 
            Address = "123 Main St, City, Country",
            Avatar = "NULL",
            Bio = "Bio of John Doe",
            Phonenumber = "123456789",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Status = "Active"
        },
        new User
        {
            UserId = 2,
            UserName = "brand_abc",
            Email = "contact@brandabc.com",
            Password = "password123",
            Address = "456 Brand St, City, Country",
            Avatar = "NULL",
            Bio = "Brand ABC official account",
            Phonenumber = "987654321",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Status = "Active"
        }
    );

            // Thêm dữ liệu mẫu cho Influencer
            modelBuilder.Entity<Influencer>().HasData(
                new Influencer
                {
                    InfluencerId = 1,
                    UserId = 1, // Liên kết với User có UserId = 1
                    Name = "Han Ni",
                    Gender = "Female",
                    DateOfBirth = new DateTime(1990,1,1),
                    BookingPrice = 1000.00m,
                    PersonalIdentificationNumber = 123456789,
                    FavouriteId = null
                }
            );

            // Thêm dữ liệu mẫu cho Brand
            modelBuilder.Entity<Brand>().HasData(
                new Brand
                {
                    BrandId = 1,
                    UserId = 2, // Liên kết với User có UserId = 2
                    BrandName = "Brand ABC",
                    ImageCover = "NULL",
                    TaxCode = "1234567890"
                }
            );
        }

    }
}