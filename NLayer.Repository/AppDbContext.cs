using Microsoft.EntityFrameworkCore;
using NLayer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=API_Db;Username=postgres;Password=123");
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }

        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }

                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x=>x.CreatedDate).IsModified = false; //güncelleme yaparken bu alana dokunma
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChanges();
        }

        //Created/UpdatedDate otomatically saved.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if(item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate= DateTime.Now;
                                break;
                            }

                        case EntityState.Modified:
                            {
                                entityReference.UpdatedDate= DateTime.Now;
                                break;
                            }
                    }
                }
            }  
            return base.SaveChangesAsync(cancellationToken);
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<ProductFeature>().HasData(
                new ProductFeature
                {
                    Id = 1,
                    Color = "Red",
                    Height = 100,
                    Width = 200,
                    ProductId = 1

                } ,

                 new ProductFeature
                 {
                     Id = 2,
                     Color = "Blue",
                     Height = 300,
                     Width = 300,
                     ProductId = 2
                 });

            base.OnModelCreating(modelBuilder);
        }

    }
}
