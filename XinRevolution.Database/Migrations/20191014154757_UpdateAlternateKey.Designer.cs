﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XinRevolution.Database;

namespace XinRevolution.Database.Migrations
{
    [DbContext(typeof(XinRevolutionContext))]
    [Migration("20191014154757_UpdateAlternateKey")]
    partial class UpdateAlternateKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("XinRevolution.Database.Entity.DumpResourceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("DumpStatus")
                        .HasColumnType("bit");

                    b.Property<string>("ResourceUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("UtcUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("ResourceUrl");

                    b.ToTable("DumpResources");
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.IssueEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Intro")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UtcUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.IssueItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("IssueId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<string>("ResourceUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UtcUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Id", "IssueId");

                    b.HasIndex("IssueId");

                    b.ToTable("IssueItems");
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.IssueRelativeLinkEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IssueId")
                        .HasColumnType("int");

                    b.Property<string>("LinkUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ResourceUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("UtcUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Id", "IssueId");

                    b.HasIndex("IssueId");

                    b.ToTable("IssueRelativeLinks");
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("UtcUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Account");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Account = "dev",
                            Address = "12345678",
                            Mail = "dev@mail.com",
                            Name = "developer",
                            Password = "dev",
                            Phone = "12345678",
                            UtcUpdateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.IssueItemEntity", b =>
                {
                    b.HasOne("XinRevolution.Database.Entity.IssueEntity", "Issue")
                        .WithMany("IssueItems")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.IssueRelativeLinkEntity", b =>
                {
                    b.HasOne("XinRevolution.Database.Entity.IssueEntity", "Issue")
                        .WithMany("IssueRelativeLinks")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
