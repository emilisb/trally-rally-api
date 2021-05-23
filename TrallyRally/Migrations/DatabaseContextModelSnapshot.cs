﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrallyRally.Data;

namespace TrallyRally.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("GamePlayer", b =>
                {
                    b.Property<int>("GamesID")
                        .HasColumnType("int");

                    b.Property<int>("PlayersID")
                        .HasColumnType("int");

                    b.HasKey("GamesID", "PlayersID");

                    b.HasIndex("PlayersID");

                    b.ToTable("GamePlayer");
                });

            modelBuilder.Entity("GameQuestion", b =>
                {
                    b.Property<int>("GamesID")
                        .HasColumnType("int");

                    b.Property<int>("QuestionsID")
                        .HasColumnType("int");

                    b.HasKey("GamesID", "QuestionsID");

                    b.HasIndex("QuestionsID");

                    b.ToTable("GameQuestion");
                });

            modelBuilder.Entity("TrallyRally.Models.Game", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("TrallyRally.Models.Player", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("StartPosition")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("TrallyRally.Models.Question", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Answer")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Image")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("Latitude")
                        .HasColumnType("double");

                    b.Property<double>("Longitude")
                        .HasColumnType("double");

                    b.Property<int>("MaxDistance")
                        .HasColumnType("int");

                    b.Property<int>("Points")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Title")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("TrallyRally.Models.QuestionSubmission", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Answer")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool?>("Correct")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<int>("GameID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PlayerID")
                        .HasColumnType("int");

                    b.Property<int>("QuestionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("GameID");

                    b.HasIndex("PlayerID");

                    b.HasIndex("QuestionID");

                    b.ToTable("QuestionSubmissions");
                });

            modelBuilder.Entity("GamePlayer", b =>
                {
                    b.HasOne("TrallyRally.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrallyRally.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameQuestion", b =>
                {
                    b.HasOne("TrallyRally.Models.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrallyRally.Models.Question", null)
                        .WithMany()
                        .HasForeignKey("QuestionsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TrallyRally.Models.QuestionSubmission", b =>
                {
                    b.HasOne("TrallyRally.Models.Game", null)
                        .WithMany("QuestionSubmissions")
                        .HasForeignKey("GameID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrallyRally.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrallyRally.Models.Question", "Question")
                        .WithMany("QuestionSubmissions")
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("TrallyRally.Models.Game", b =>
                {
                    b.Navigation("QuestionSubmissions");
                });

            modelBuilder.Entity("TrallyRally.Models.Question", b =>
                {
                    b.Navigation("QuestionSubmissions");
                });
#pragma warning restore 612, 618
        }
    }
}
