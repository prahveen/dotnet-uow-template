using Microsoft.EntityFrameworkCore;
using Template.Entities.Entities;
using Template.Entities.Entities.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Entities.Context
{
   public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new SearchKeywordMap(modelBuilder.Entity<SearchKeyword>());
            modelBuilder.Entity<BaseEntity>().Property(p => p.RowVersion).IsConcurrencyToken().IsRequired().IsRowVersion();
        }
        public DbSet<SearchKeyword> SearchKeywords { get; set; }
    }
}
