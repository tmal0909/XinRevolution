using Microsoft.EntityFrameworkCore;
using XinRevolution.Database.Entity;
using XinRevolution.Database.Entity.FireGeneration;

namespace XinRevolution.Database
{
    public class XinRevolutionContext : DbContext
    {
        #region DbSet

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<DumpResourceEntity> DumpResources { get; set; }

        public DbSet<IssueEntity> Issues { get; set; }

        public DbSet<IssueItemEntity> IssueItems { get; set; }

        public DbSet<IssueRelativeLinkEntity> IssueRelativeLinks { get; set; }

        public DbSet<TagEntity> Tags { get; set; }

        public DbSet<BlogEntity> Blogs { get; set; }

        public DbSet<BlogPostEntity> BlogPosts { get; set; }

        public DbSet<BlogTagEntity> BlogTags { get; set; }

        public DbSet<WorkEntity> Works { get; set; }

        #endregion

        #region Constructor

        public XinRevolutionContext(DbContextOptions<XinRevolutionContext> options) : base(options) { }

        #endregion

        #region Fluent API

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
            modelBuilder.Entity<WorkEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<FGGroupEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<FGGroupRoleEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<FGRoleEquipmentEntity>().HasKey(x => x.Id);
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
            modelBuilder.Entity<WorkEntity>().HasAlternateKey(x => new { x.Name });
            modelBuilder.Entity<FGGroupEntity>().HasAlternateKey(x => new { x.Name });
            modelBuilder.Entity<FGGroupRoleEntity>().HasAlternateKey(x => new { x.Id, x.GroupId });
            modelBuilder.Entity<FGRoleEquipmentEntity>().HasAlternateKey(x => new { x.Id, x.RoleId });
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
            modelBuilder.Entity<WorkEntity>().Property(x => x.UtcUpdateTime).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<FGGroupEntity>().Property(x => x.UtcUpdateTime).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<FGGroupRoleEntity>().Property(x => x.UtcUpdateTime).HasDefaultValueSql("getutcdate()");
            modelBuilder.Entity<FGRoleEquipmentEntity>().Property(x => x.UtcUpdateTime).HasDefaultValueSql("getutcdate()");
        }

        private void DefineRelation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IssueItemEntity>().HasOne(x => x.Issue).WithMany(x => x.IssueItems).HasForeignKey(x => x.IssueId);
            modelBuilder.Entity<IssueRelativeLinkEntity>().HasOne(x => x.Issue).WithMany(x => x.IssueRelativeLinks).HasForeignKey(x => x.IssueId);
            modelBuilder.Entity<BlogPostEntity>().HasOne(x => x.Blog).WithMany(x => x.BlogPosts).HasForeignKey(x => x.BlogId);
            modelBuilder.Entity<BlogTagEntity>().HasOne(x => x.Blog).WithMany(x => x.BlogTags).HasForeignKey(x => x.BlogId);
            modelBuilder.Entity<BlogTagEntity>().HasOne(x => x.Tag).WithMany(x => x.BlogTags).HasForeignKey(x => x.TagId);
            modelBuilder.Entity<FGGroupRoleEntity>().HasOne(x => x.Group).WithMany(x => x.Roles).HasForeignKey(x => x.GroupId);
            modelBuilder.Entity<FGRoleEquipmentEntity>().HasOne(x => x.Role).WithMany(x => x.Equipments).HasForeignKey(x => x.RoleId);
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

        #endregion
    }
}
