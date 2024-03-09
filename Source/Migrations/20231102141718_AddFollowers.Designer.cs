﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TDP.Models.Persistence;

#nullable disable

namespace TDP.Migrations
{
    [DbContext(typeof(TdpDbContext))]
    [Migration("20231102141718_AddFollowers")]
    partial class AddFollowers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.2.22153.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MovieUser", b =>
                {
                    b.Property<Guid>("FollowedMoviesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("FollowersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("FollowedMoviesId", "FollowersId");

                    b.HasIndex("FollowersId");

                    b.ToTable("MovieUser");
                });

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

                    b.Property<string>("ImdbId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ImdbRating")
                        .HasPrecision(4, 2)
                        .HasColumnType("decimal(4,2)");

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

                    b.HasKey("Id");

                    b.ToTable("Movie");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Movie");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8f38966e-dfa8-45d9-8d4f-b86b330786cd"),
                            Country = "United States",
                            ImdbId = "",
                            ImdbRating = 9.3m,
                            Plot = "Some not really important plot",
                            PosterUrl = "",
                            Released = new DateTime(2012, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Runtime = 250L,
                            Title = "Avengers",
                            Type = "Movie"
                        },
                        new
                        {
                            Id = new Guid("0de8d323-53d4-48ad-a1ff-8a91dd141595"),
                            Country = "Somalia",
                            ImdbId = "",
                            ImdbRating = 8.3m,
                            Plot = "Some not really important plot",
                            PosterUrl = "",
                            Released = new DateTime(2013, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Runtime = 220L,
                            Title = "Batman",
                            Type = "Movie"
                        });
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

                    b.Property<bool>("IsAdministrator")
                        .HasColumnType("bit");

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

                    b.HasData(
                        new
                        {
                            Id = new Guid("c06d24c3-2303-47ff-85c2-64def885d63d"),
                            BirthDay = new DateTime(1945, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmailAddress = "elYusty@bokita.com",
                            IsAdministrator = false,
                            LastName = "Fabra",
                            Name = "Yusty",
                            PasswordHash = "boca",
                            Username = "elyusty"
                        });
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

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("Season")
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

            modelBuilder.Entity("MovieUser", b =>
                {
                    b.HasOne("TDP.Models.Domain.Movie", null)
                        .WithMany()
                        .HasForeignKey("FollowedMoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TDP.Models.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("FollowersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TDP.Models.Domain.Movie", b =>
                {
                    b.OwnsMany("TDP.Models.Domain.MovieParticipant", "Participants", b1 =>
                        {
                            b1.Property<Guid>("MovieId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("Role")
                                .HasColumnType("int");

                            b1.HasKey("MovieId", "Name", "Role");

                            b1.ToTable("MovieParticipant");

                            b1.WithOwner("Movie")
                                .HasForeignKey("MovieId");

                            b1.Navigation("Movie");
                        });

                    b.Navigation("Participants");
                });

            modelBuilder.Entity("TDP.Models.Domain.UserRating", b =>
                {
                    b.HasOne("TDP.Models.Domain.Movie", "Movie")
                        .WithMany("Ratings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TDP.Models.Domain.User", "User")
                        .WithMany("RatedMovies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TDP.Models.Domain.Episode", b =>
                {
                    b.HasOne("TDP.Models.Domain.Series", "Series")
                        .WithMany("Episodes")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Series");
                });

            modelBuilder.Entity("TDP.Models.Domain.Movie", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("TDP.Models.Domain.User", b =>
                {
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
