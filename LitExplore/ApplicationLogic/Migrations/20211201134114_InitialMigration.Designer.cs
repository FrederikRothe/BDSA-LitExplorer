﻿// <auto-generated />
using System;
using LitExplore.ApplicationLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LitExplore.ApplicationLogic.Migrations
{
    [DbContext(typeof(LitExploreContext))]
    [Migration("20211201134114_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LitExplore.ApplicationLogic.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int?>("PaperId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaperId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("LitExplore.ApplicationLogic.Connection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConnectionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Paper1Id")
                        .HasColumnType("int");

                    b.Property<int>("Paper2Id")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Paper1Id");

                    b.HasIndex("Paper2Id");

                    b.HasIndex("UserId");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("LitExplore.ApplicationLogic.Paper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Papers");
                });

            modelBuilder.Entity("LitExplore.ApplicationLogic.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PaperId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaperId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("LitExplore.ApplicationLogic.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("TeamLeaderId")
                        .HasColumnType("int");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("TeamLeaderId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("LitExplore.ApplicationLogic.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LitExplore.ApplicationLogic.Author", b =>
                {
                    b.HasOne("LitExplore.ApplicationLogic.Paper", null)
                        .WithMany("Authors")
                        .HasForeignKey("PaperId");
                });

            modelBuilder.Entity("LitExplore.ApplicationLogic.Connection", b =>
                {
                    b.HasOne("LitExplore.ApplicationLogic.Paper", "Paper1")
                        .WithMany()
                        .HasForeignKey("Paper1Id");

                    b.HasOne("LitExplore.ApplicationLogic.Paper", "Paper2")
                        .WithMany()
                        .HasForeignKey("Paper2Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LitExplore.ApplicationLogic.User", null)
                        .WithMany("Connections")
                        .HasForeignKey("UserId");

                    b.Navigation("Paper1");

                    b.Navigation("Paper2");
                });

            modelBuilder.Entity("LitExplore.ApplicationLogic.Tag", b =>
                {
                    b.HasOne("LitExplore.ApplicationLogic.Paper", null)
                        .WithMany("Tags")
                        .HasForeignKey("PaperId");
                });

            modelBuilder.Entity("LitExplore.ApplicationLogic.Team", b =>
                {
                    b.HasOne("LitExplore.ApplicationLogic.User", "TeamLeader")
                        .WithMany()
                        .HasForeignKey("TeamLeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TeamLeader");
                });

            modelBuilder.Entity("LitExplore.ApplicationLogic.User", b =>
                {
                    b.HasOne("LitExplore.ApplicationLogic.Team", null)
                        .WithMany("Users")
                        .HasForeignKey("TeamId");
                });

            modelBuilder.Entity("LitExplore.ApplicationLogic.Paper", b =>
                {
                    b.Navigation("Authors");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("LitExplore.ApplicationLogic.Team", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("LitExplore.ApplicationLogic.User", b =>
                {
                    b.Navigation("Connections");
                });
#pragma warning restore 612, 618
        }
    }
}
