using Microsoft.EntityFrameworkCore;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DefineKey(modelBuilder);
            DefineUniqueKey(modelBuilder);
            DefineRelation(modelBuilder);
            DefineSeedData(modelBuilder);
        }

        private void DefineKey(ModelBuilder modelBuilder)
        {

        }

        private void DefineUniqueKey(ModelBuilder modelBuilder)
        {

        }

        private void DefineRelation(ModelBuilder modelBuilder)
        {

        }

        private void DefineSeedData(ModelBuilder modelBuilder)
        {

        }
    }
}
