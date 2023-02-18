﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TDP.Models.Persistence;

#nullable disable

namespace TDP.Migrations
{
    [DbContext(typeof(TdpDbContext))]
    partial class TdpDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.2.22153.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TDP.Models.Domain.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("IMDBRating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Plot")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Released")
                        .HasColumnType("datetime2");

                    b.Property<long>("Runtime")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Movie");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Movie");
                });

            modelBuilder.Entity("TDP.Models.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("TDP.Models.Domain.UserRating", b =>
                {
                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("MovieId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRating");
                });

            modelBuilder.Entity("TDP.Models.Domain.Episode", b =>
                {
                    b.HasBaseType("TDP.Models.Domain.Movie");

                    b.Property<int>("EpisodeNumber")
                        .HasColumnType("int");

                    b.Property<int>("SeasonNumber")
                        .HasColumnType("int");

                    b.Property<Guid>("SeriesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("SeriesId");

                    b.HasDiscriminator().HasValue("Episode");
                });

            modelBuilder.Entity("TDP.Models.Domain.Series", b =>
                {
                    b.HasBaseType("TDP.Models.Domain.Movie");

                    b.Property<int>("Seasons")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Series");
                });

            modelBuilder.Entity("TDP.Models.Domain.Movie", b =>
                {
                    b.HasOne("TDP.Models.Domain.User", null)
                        .WithMany("FollowedMovies")
                        .HasForeignKey("UserId");

                    b.OwnsMany("TDP.Models.Domain.MovieParticipant", "Participants", b1 =>
                        {
                            b1.Property<Guid>("MovieId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Role")
                                .HasColumnType("nvarchar(450)");

                            b1.HasKey("MovieId", "Name", "Role");

                            b1.ToTable("MovieParticipant");

                            b1.WithOwner()
                                .HasForeignKey("MovieId");
                        });

                    b.Navigation("Participants");
                });

            modelBuilder.Entity("TDP.Models.Domain.UserRating", b =>
                {
                    b.HasOne("TDP.Models.Domain.Movie", null)
                        .WithMany("Ratings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TDP.Models.Domain.User", null)
                        .WithMany("RatedMovies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TDP.Models.Domain.Episode", b =>
                {
                    b.HasOne("TDP.Models.Domain.Series", "Series")
                        .WithMany("Episodes")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Series");
                });

            modelBuilder.Entity("TDP.Models.Domain.Movie", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("TDP.Models.Domain.User", b =>
                {
                    b.Navigation("FollowedMovies");

                    b.Navigation("RatedMovies");
                });

            modelBuilder.Entity("TDP.Models.Domain.Series", b =>
                {
                    b.Navigation("Episodes");
                });
#pragma warning restore 612, 618
        }
    }
}