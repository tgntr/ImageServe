﻿// <auto-generated />
using System;
using ImageServe.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ImageServe.Data.Migrations
{
    [DbContext(typeof(ImageServeDbContext))]
    [Migration("20190314033017_ChangeImageAndTagKeys")]
    partial class ChangeImageAndTagKeys
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ImageServe.Models.Friendship", b =>
                {
                    b.Property<string>("FriendId");

                    b.Property<string>("UserId");

                    b.HasKey("FriendId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("ImageServe.Models.Image", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateUploaded")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description");

                    b.Property<bool>("IsPublic");

                    b.Property<string>("UserId");

                    b.HasKey("Name");

                    b.HasIndex("UserId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ImageServe.Models.ImageTag", b =>
                {
                    b.Property<string>("Name");

                    b.Property<string>("ImageName");

                    b.Property<bool>("FromDescription");

                    b.HasKey("Name", "ImageName");

                    b.HasIndex("ImageName");

                    b.ToTable("ImageTags");
                });

            modelBuilder.Entity("ImageServe.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Details");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ImageServe.Models.Friendship", b =>
                {
                    b.HasOne("ImageServe.Models.User", "Friend")
                        .WithMany("FriendUsers")
                        .HasForeignKey("FriendId");

                    b.HasOne("ImageServe.Models.User", "User")
                        .WithMany("UserFriends")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ImageServe.Models.Image", b =>
                {
                    b.HasOne("ImageServe.Models.User", "User")
                        .WithMany("Images")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ImageServe.Models.ImageTag", b =>
                {
                    b.HasOne("ImageServe.Models.Image", "Image")
                        .WithMany("Tags")
                        .HasForeignKey("ImageName");
                });
#pragma warning restore 612, 618
        }
    }
}
