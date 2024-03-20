﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("Domain.GameRound", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Clue")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameSessionID")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoundNumber")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("SpectrumCardID")
                        .HasColumnType("TEXT");

                    b.Property<int>("TargetOffset")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("TeamTurn")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("SpectrumCardID");

                    b.ToTable("GameRounds", (string)null);
                });

            modelBuilder.Entity("Domain.GameRoundGhostGuess", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameRoundID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameSessionID")
                        .HasColumnType("TEXT");

                    b.Property<int>("TargetOffset")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Team")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("GameRoundID");

                    b.ToTable("GameRoundGhostGuesses", (string)null);
                });

            modelBuilder.Entity("Domain.GameRoundOpposingTeamGuess", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameRoundID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameSessionID")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsLeft")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Team")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("GameRoundID");

                    b.ToTable("GameRoundOpposingTeamGuesses", (string)null);
                });

            modelBuilder.Entity("Domain.GameRoundOpposingTeamSelection", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameRoundID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameSessionID")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsLeft")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Team")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("GameRoundID")
                        .IsUnique();

                    b.ToTable("GameRoundOpposingTeamSelections", (string)null);
                });

            modelBuilder.Entity("Domain.GameRoundSelectorSelection", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameRoundID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameSessionID")
                        .HasColumnType("TEXT");

                    b.Property<int>("TargetOffset")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Team")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("GameRoundID")
                        .IsUnique();

                    b.ToTable("GameRoundSelectorSelections", (string)null);
                });

            modelBuilder.Entity("Domain.GameSession", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("GameRound")
                        .HasColumnType("INTEGER");

                    b.Property<string>("JoinCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("OwnerID")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("GameSessions", (string)null);
                });

            modelBuilder.Entity("Domain.GameSessionMember", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameSessionID")
                        .HasColumnType("TEXT");

                    b.Property<byte>("Team")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("GameSessionID");

                    b.ToTable("GameSessionMembers", (string)null);
                });

            modelBuilder.Entity("Domain.GameSessionMemberRoundRole", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameRoundID")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameSessionID")
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("Team")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("UserID")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.HasIndex("GameRoundID");

                    b.ToTable("GameSessionMemberRoundRoles", (string)null);
                });

            modelBuilder.Entity("Domain.GameSessionResult", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GameSessionID")
                        .HasColumnType("TEXT");

                    b.Property<int>("LosingScore")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("LosingTeam")
                        .HasColumnType("INTEGER");

                    b.Property<int>("WinningScore")
                        .HasColumnType("INTEGER");

                    b.Property<byte>("WinningTeam")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("GameSessionID")
                        .IsUnique();

                    b.ToTable("GameSessionResults", (string)null);
                });

            modelBuilder.Entity("Domain.SpectrumCard", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("LeftName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RightName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("SpectrumCards", (string)null);

                    b.HasData(
                        new
                        {
                            ID = new Guid("d443399e-4292-45d3-903e-937743e049d3"),
                            LeftName = "Good",
                            RightName = "Bad"
                        },
                        new
                        {
                            ID = new Guid("8e4a470c-1df2-4bf5-a034-2004628eae90"),
                            LeftName = "Highly Attractive",
                            RightName = "Mildly Attractive"
                        },
                        new
                        {
                            ID = new Guid("c5150589-e161-4096-9c73-e72b75e8d0e7"),
                            LeftName = "Cold",
                            RightName = "Hot"
                        },
                        new
                        {
                            ID = new Guid("77b7290e-583f-463d-b643-b961d353e7f3"),
                            LeftName = "Weird",
                            RightName = "Normal"
                        },
                        new
                        {
                            ID = new Guid("4508e7d3-2d87-4d3f-9811-a80ceef6c14b"),
                            LeftName = "Colorful",
                            RightName = "Colorless"
                        },
                        new
                        {
                            ID = new Guid("ca7c9ab3-71b1-4376-a452-4e1ce108e070"),
                            LeftName = "High Calorie",
                            RightName = "Low Calorie"
                        },
                        new
                        {
                            ID = new Guid("d737e617-8b15-48aa-84b6-80eb11b8d09a"),
                            LeftName = "Feels Good",
                            RightName = "Feels Bad"
                        },
                        new
                        {
                            ID = new Guid("a32543d2-1da3-4d39-b533-66014de89889"),
                            LeftName = "Expensive",
                            RightName = "Cheap"
                        },
                        new
                        {
                            ID = new Guid("af711bff-924a-407b-b712-99e13b0cbf9f"),
                            LeftName = "Overrated Weapon",
                            RightName = "Underrated Weapon"
                        },
                        new
                        {
                            ID = new Guid("4776e959-b795-4ec1-aa5f-440d786387d3"),
                            LeftName = "Common",
                            RightName = "Rare"
                        });
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("AvatarID")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.GameRound", b =>
                {
                    b.HasOne("Domain.SpectrumCard", "SpectrumCard")
                        .WithMany()
                        .HasForeignKey("SpectrumCardID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SpectrumCard");
                });

            modelBuilder.Entity("Domain.GameRoundGhostGuess", b =>
                {
                    b.HasOne("Domain.GameRound", "GameRound")
                        .WithMany("GhostGuesses")
                        .HasForeignKey("GameRoundID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameRound");
                });

            modelBuilder.Entity("Domain.GameRoundOpposingTeamGuess", b =>
                {
                    b.HasOne("Domain.GameRound", "GameRound")
                        .WithMany("OpposingGhostGuesses")
                        .HasForeignKey("GameRoundID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameRound");
                });

            modelBuilder.Entity("Domain.GameRoundOpposingTeamSelection", b =>
                {
                    b.HasOne("Domain.GameRound", "GameRound")
                        .WithOne("OpposingSelectorSelection")
                        .HasForeignKey("Domain.GameRoundOpposingTeamSelection", "GameRoundID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameRound");
                });

            modelBuilder.Entity("Domain.GameRoundSelectorSelection", b =>
                {
                    b.HasOne("Domain.GameRound", "GameRound")
                        .WithOne("SelectorSelection")
                        .HasForeignKey("Domain.GameRoundSelectorSelection", "GameRoundID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameRound");
                });

            modelBuilder.Entity("Domain.GameSessionMember", b =>
                {
                    b.HasOne("Domain.GameSession", "GameSession")
                        .WithMany("Members")
                        .HasForeignKey("GameSessionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameSession");
                });

            modelBuilder.Entity("Domain.GameSessionMemberRoundRole", b =>
                {
                    b.HasOne("Domain.GameRound", null)
                        .WithMany("RoundRoles")
                        .HasForeignKey("GameRoundID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.GameSessionResult", b =>
                {
                    b.HasOne("Domain.GameSession", "GameSession")
                        .WithOne("GameSessionResult")
                        .HasForeignKey("Domain.GameSessionResult", "GameSessionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameSession");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.GameRound", b =>
                {
                    b.Navigation("GhostGuesses");

                    b.Navigation("OpposingGhostGuesses");

                    b.Navigation("OpposingSelectorSelection")
                        .IsRequired();

                    b.Navigation("RoundRoles");

                    b.Navigation("SelectorSelection")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.GameSession", b =>
                {
                    b.Navigation("GameSessionResult")
                        .IsRequired();

                    b.Navigation("Members");
                });
#pragma warning restore 612, 618
        }
    }
}
