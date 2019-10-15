﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using XinRevolution.Database.Entity;

namespace XinRevolution.Database
{
    public class XinRevolutionContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public DbSet<DumpResourceEntity> DumpResources { get; set; }

        public DbSet<IssueEntity> Issues { get; set; }

        public DbSet<IssueItemEntity> IssueItems { get; set; }

        public DbSet<IssueRelativeLinkEntity> IssueRelativeLinks { get; set; }

        public DbSet<TagEntity> Tags { get; set; }

        public DbSet<BlogEntity> Blogs { get; set; }

        public DbSet<BlogPostEntity> BlogPosts { get; set; }

        public DbSet<BlogTagEntity> BlogTags { get; set; }

        public XinRevolutionContext(DbContextOptions<XinRevolutionContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DefineKey(modelBuilder);
            DefineAlternateKey(modelBuilder);
            DefineDefaultValue(modelBuilder);
            DefineRelation(modelBuilder);
            DefineSeedData(modelBuilder);
        }

        private void DefineKey(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DumpResourceEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<UserEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<IssueEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<IssueItemEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<IssueRelativeLinkEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<TagEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<BlogEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<BlogPostEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<BlogTagEntity>().HasKey(x => x.Id);
        }

        private void DefineAlternateKey(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DumpResourceEntity>().HasAlternateKey(x => new { x.ResourceUrl });
            modelBuilder.Entity<UserEntity>().HasAlternateKey(x => new { x.Account });
            modelBuilder.Entity<IssueEntity>().HasAlternateKey(x => new { x.Name });
            modelBuilder.Entity<IssueItemEntity>().HasAlternateKey(x => new { x.Id, x.IssueId });
            modelBuilder.Entity<IssueRelativeLinkEntity>().HasAlternateKey(x => new { x.Id, x.IssueId });
            modelBuilder.Entity<TagEntity>().HasAlternateKey(x => new { x.Name });
            modelBuilder.Entity<BlogEntity>().HasAlternateKey(x => new { x.Name });
            modelBuilder.Entity<BlogPostEntity>().HasAlternateKey(x => new { x.Id, x.BlogId });
            modelBuilder.Entity<BlogTagEntity>().HasAlternateKey(x => new { x.Id, x.BlogId, x.TagId });
        }

        private void DefineDefaultValue(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DumpResourceEntity>().Property(x => x.UtcUpdateTime).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<UserEntity>().Property(x => x.UtcUpdateTime).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<IssueEntity>().Property(x => x.UtcUpdateTime).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<IssueItemEntity>().Property(x => x.UtcUpdateTime).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<IssueRelativeLinkEntity>().Property(x => x.UtcUpdateTime).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<TagEntity>().Property(x => x.UtcUpdateTime).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<BlogEntity>().Property(x => x.UtcUpdateTime).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<BlogPostEntity>().Property(x => x.UtcUpdateTime).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<BlogTagEntity>().Property(x => x.UtcUpdateTime).HasDefaultValueSql("getutcdate()");
        }

        private void DefineRelation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IssueItemEntity>().HasOne(x => x.Issue).WithMany(x => x.IssueItems).HasForeignKey(x => x.IssueId);
            modelBuilder.Entity<IssueRelativeLinkEntity>().HasOne(x => x.Issue).WithMany(x => x.IssueRelativeLinks).HasForeignKey(x => x.IssueId);
            modelBuilder.Entity<BlogPostEntity>().HasOne(x => x.Blog).WithMany(x => x.BlogPosts).HasForeignKey(x => x.BlogId);
            modelBuilder.Entity<BlogTagEntity>().HasOne(x => x.Blog).WithMany(x => x.BlogTags).HasForeignKey(x => x.BlogId);
            modelBuilder.Entity<BlogTagEntity>().HasOne(x => x.Tag).WithMany(x => x.BlogTags).HasForeignKey(x => x.TagId);
        }

        private void DefineSeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(new UserEntity
            {
                Id = 1,
                Account = "dev",
                Password = "dev",
                Name = "developer",
                Phone = "12345678",
                Mail = "dev@mail.com",
                Address = "12345678"
            });
        }
    }
}
