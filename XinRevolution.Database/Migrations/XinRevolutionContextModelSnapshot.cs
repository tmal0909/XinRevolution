﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XinRevolution.Database;

namespace XinRevolution.Database.Migrations
{
    [DbContext(typeof(XinRevolutionContext))]
    partial class XinRevolutionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("XinRevolution.Database.Entity.BlogEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("UtcUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.BlogPostEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlogId")
                        .HasColumnType("int");

                    b.Property<string>("ReferenceContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<short>("ReferenceType")
                        .HasColumnType("smallint");

                    b.Property<short>("Sort")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("UtcUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Id", "BlogId");

                    b.HasIndex("BlogId");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.BlogTagEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BlogId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UtcUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Id", "BlogId", "TagId");

                    b.HasIndex("BlogId");

                    b.HasIndex("TagId");

                    b.ToTable("BlogTags");
                });

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

            modelBuilder.Entity("XinRevolution.Database.Entity.FireGeneration.FGGroupEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ResourceUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<DateTime>("UtcUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("FGGroups");
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.FireGeneration.FGGroupRoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Intro")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RelativeLinkUrl")
                        .HasColumnType("nvarchar(300)");

                    b.Property<short>("Sort")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("UtcUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Id", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("FGGroupRoles");
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.FireGeneration.FGRoleEquipmentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Intro")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("MainResourceUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("SlideResourceUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<short>("Sort")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("UtcUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Id", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("FGRoleEquipments");
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.FireGeneration.FGRoleResourceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ResourceUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<short>("Sort")
                        .HasColumnType("smallint");

                    b.Property<short>("Type")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("UtcUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Id", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("FGRoleResources");
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

            modelBuilder.Entity("XinRevolution.Database.Entity.TagEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UtcUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Tags");
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

            modelBuilder.Entity("XinRevolution.Database.Entity.WorkEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Controller")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Intro")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ResourceUrl")
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<DateTime>("UtcUpdateTime")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Works");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Controller = "FireGeneration",
                            Intro = "焰世代作品簡介",
                            Name = "焰世代",
                            ResourceUrl = "default",
                            Sort = 0,
                            UtcUpdateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.BlogPostEntity", b =>
                {
                    b.HasOne("XinRevolution.Database.Entity.BlogEntity", "Blog")
                        .WithMany("BlogPosts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.BlogTagEntity", b =>
                {
                    b.HasOne("XinRevolution.Database.Entity.BlogEntity", "Blog")
                        .WithMany("BlogTags")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("XinRevolution.Database.Entity.TagEntity", "Tag")
                        .WithMany("BlogTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.FireGeneration.FGGroupRoleEntity", b =>
                {
                    b.HasOne("XinRevolution.Database.Entity.FireGeneration.FGGroupEntity", "Group")
                        .WithMany("Roles")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.FireGeneration.FGRoleEquipmentEntity", b =>
                {
                    b.HasOne("XinRevolution.Database.Entity.FireGeneration.FGGroupRoleEntity", "Role")
                        .WithMany("Equipments")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("XinRevolution.Database.Entity.FireGeneration.FGRoleResourceEntity", b =>
                {
                    b.HasOne("XinRevolution.Database.Entity.FireGeneration.FGGroupRoleEntity", "Role")
                        .WithMany("Resources")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
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
