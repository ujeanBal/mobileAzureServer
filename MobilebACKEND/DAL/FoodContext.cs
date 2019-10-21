using Microsoft.Azure.Mobile.Server.Tables;
using MobilebACKEND.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace MobilebACKEND.DAL
{
    public class FoodContext : DbContext
    {
        public FoodContext()
            : base("FoodContext")
        {
        }

        public DbSet<Food> Foods { get; set; }

        public DbSet<PFC> PFCs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));

            modelBuilder.Entity<Food>().HasKey(e => e.Id);
            modelBuilder.Entity<PFC>().HasKey(e => e.Id);
            modelBuilder.Entity<PFC>()
                .HasRequired(e => e.Owner)
                .WithRequiredDependent(e => e.Description)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<Food>().ToTable("Food");
            modelBuilder.Entity<PFC>().ToTable("Pfc");
        }
    }
}