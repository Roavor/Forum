using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebApplication1.Models
{
    public class ListThemes : DbContext
    {
        public DbSet<Themes> LThemes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<ListThemes>(new DropCreateDatabaseIfModelChanges<ListThemes>());

            modelBuilder.Entity<Themes>().ToTable("Themes");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}