﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241113143021_UpdateFavouriteValue")]
    partial class UpdateFavouriteValue
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BrandCategory", b =>
                {
                    b.Property<int>("BrandsBrandId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriesCategoryId")
                        .HasColumnType("int");

                    b.HasKey("BrandsBrandId", "CategoriesCategoryId");

                    b.HasIndex("CategoriesCategoryId");

                    b.ToTable("BrandCategory");
                });

            modelBuilder.Entity("CampaignCategory", b =>
                {
                    b.Property<int>("CampaignsCampaignId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriesCategoryId")
                        .HasColumnType("int");

                    b.HasKey("CampaignsCampaignId", "CategoriesCategoryId");

                    b.HasIndex("CategoriesCategoryId");

                    b.ToTable("CampaignCategory");
                });

            modelBuilder.Entity("CategoryInfluencer", b =>
                {
                    b.Property<int>("CategoriesCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("InfluencersInfluencerId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesCategoryId", "InfluencersInfluencerId");

                    b.HasIndex("InfluencersInfluencerId");

                    b.ToTable("CategoryInfluencer");
                });

            modelBuilder.Entity("api.Models.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandId"));

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageCover")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BrandId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            BrandId = 1,
                            BrandName = "Brand ABC",
                            ImageCover = "NULL",
                            TaxCode = "1234567890",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("api.Models.Campaign", b =>
                {
                    b.Property<int>("CampaignId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CampaignId"));

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("Budget")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FaviconId")
                        .HasColumnType("int");

                    b.Property<int?>("FavouriteId")
                        .HasColumnType("int");

                    b.Property<string>("Requirements")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CampaignId");

                    b.HasIndex("BrandId");

                    b.HasIndex("FavouriteId");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("api.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<int?>("CampaignId")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InfluencerId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("api.Models.Favourite", b =>
                {
                    b.Property<int>("FavouriteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FavouriteId"));

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<int>("InfluencerId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FavouriteId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("api.Models.Influencer", b =>
                {
                    b.Property<int>("InfluencerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InfluencerId"));

                    b.Property<decimal>("BookingPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int?>("FavouriteId")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonalIdentificationNumber")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("InfluencerId");

                    b.HasIndex("FavouriteId");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.ToTable("Influencers");

                    b.HasData(
                        new
                        {
                            InfluencerId = 1,
                            BookingPrice = 1000.00m,
                            DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Gender = "Female",
                            Name = "Han Ni",
                            PersonalIdentificationNumber = 123456789,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("api.Models.Proposal", b =>
                {
                    b.Property<int>("ProposalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProposalId"));

                    b.Property<int?>("CampaignId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InfluencerId")
                        .HasColumnType("int");

                    b.Property<string>("OfferDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ProposalId");

                    b.HasIndex("CampaignId");

                    b.HasIndex("InfluencerId");

                    b.ToTable("Proposals");
                });

            modelBuilder.Entity("api.Models.SocialMedia", b =>
                {
                    b.Property<int>("SocialMediaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SocialMediaId"));

                    b.Property<int>("FollowersCount")
                        .HasColumnType("int");

                    b.Property<int?>("InfluencerId")
                        .HasColumnType("int");

                    b.Property<string>("SocialMediaImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialMediaLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialMediaName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SocialMediaId");

                    b.HasIndex("InfluencerId");

                    b.ToTable("SocialMedia");
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FavoriteId")
                        .HasColumnType("int");

                    b.Property<int?>("FavouriteId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phonenumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("FavouriteId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Address = "123 Main St, City, Country",
                            Avatar = "NULL",
                            Bio = "Bio of John Doe",
                            CreatedAt = new DateTime(2024, 11, 13, 21, 30, 19, 324, DateTimeKind.Local).AddTicks(9435),
                            Email = "ni.trnh59@gmail.com",
                            Password = "Abc123#",
                            Phonenumber = "123456789",
                            Status = "Active",
                            UpdatedAt = new DateTime(2024, 11, 13, 21, 30, 19, 324, DateTimeKind.Local).AddTicks(9467),
                            UserName = "hanni_44"
                        },
                        new
                        {
                            UserId = 2,
                            Address = "456 Brand St, City, Country",
                            Avatar = "NULL",
                            Bio = "Brand ABC official account",
                            CreatedAt = new DateTime(2024, 11, 13, 21, 30, 19, 324, DateTimeKind.Local).AddTicks(9471),
                            Email = "contact@brandabc.com",
                            Password = "password123",
                            Phonenumber = "987654321",
                            Status = "Active",
                            UpdatedAt = new DateTime(2024, 11, 13, 21, 30, 19, 324, DateTimeKind.Local).AddTicks(9472),
                            UserName = "brand_abc"
                        });
                });

            modelBuilder.Entity("BrandCategory", b =>
                {
                    b.HasOne("api.Models.Brand", null)
                        .WithMany()
                        .HasForeignKey("BrandsBrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CampaignCategory", b =>
                {
                    b.HasOne("api.Models.Campaign", null)
                        .WithMany()
                        .HasForeignKey("CampaignsCampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CategoryInfluencer", b =>
                {
                    b.HasOne("api.Models.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.Influencer", null)
                        .WithMany()
                        .HasForeignKey("InfluencersInfluencerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.Brand", b =>
                {
                    b.HasOne("api.Models.User", "User")
                        .WithOne("Brand")
                        .HasForeignKey("api.Models.Brand", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("api.Models.Campaign", b =>
                {
                    b.HasOne("api.Models.Brand", "Brand")
                        .WithMany("Campaigns")
                        .HasForeignKey("BrandId");

                    b.HasOne("api.Models.Favourite", "Favourite")
                        .WithMany("Campaigns")
                        .HasForeignKey("FavouriteId");

                    b.Navigation("Brand");

                    b.Navigation("Favourite");
                });

            modelBuilder.Entity("api.Models.Influencer", b =>
                {
                    b.HasOne("api.Models.Favourite", "Favourite")
                        .WithMany("Influencers")
                        .HasForeignKey("FavouriteId");

                    b.HasOne("api.Models.User", "User")
                        .WithOne("Influencer")
                        .HasForeignKey("api.Models.Influencer", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Favourite");

                    b.Navigation("User");
                });

            modelBuilder.Entity("api.Models.Proposal", b =>
                {
                    b.HasOne("api.Models.Campaign", "Campaign")
                        .WithMany("Proposals")
                        .HasForeignKey("CampaignId");

                    b.HasOne("api.Models.Influencer", "Influencer")
                        .WithMany("Proposals")
                        .HasForeignKey("InfluencerId");

                    b.Navigation("Campaign");

                    b.Navigation("Influencer");
                });

            modelBuilder.Entity("api.Models.SocialMedia", b =>
                {
                    b.HasOne("api.Models.Influencer", null)
                        .WithMany("SocialMedias")
                        .HasForeignKey("InfluencerId");
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.HasOne("api.Models.Favourite", "Favourite")
                        .WithMany("Users")
                        .HasForeignKey("FavouriteId");

                    b.Navigation("Favourite");
                });

            modelBuilder.Entity("api.Models.Brand", b =>
                {
                    b.Navigation("Campaigns");
                });

            modelBuilder.Entity("api.Models.Campaign", b =>
                {
                    b.Navigation("Proposals");
                });

            modelBuilder.Entity("api.Models.Favourite", b =>
                {
                    b.Navigation("Campaigns");

                    b.Navigation("Influencers");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("api.Models.Influencer", b =>
                {
                    b.Navigation("Proposals");

                    b.Navigation("SocialMedias");
                });

            modelBuilder.Entity("api.Models.User", b =>
                {
                    b.Navigation("Brand");

                    b.Navigation("Influencer");
                });
#pragma warning restore 612, 618
        }
    }
}
