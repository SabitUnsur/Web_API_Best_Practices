using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                
                new Product 
                { ID = 1, 
                    CategoryId = 1, 
                    Name= "Kalem1" ,
                    Price = 100, 
                    Stock = 20, 
                    CreatedDate = DateTime.SpecifyKind(DateTime.Parse(DateTime.Now.ToString()), DateTimeKind.Local)

                },
                
                new Product 
                { ID = 2, 
                    CategoryId = 1, 
                    Name = "Kalem2" , 
                    Price = 200, 
                    Stock = 30, 
                    CreatedDate = DateTime.SpecifyKind(DateTime.Parse(DateTime.Now.ToString()), DateTimeKind.Local)
                } ,

                new Product
                { ID = 3, 
                    CategoryId = 1, 
                    Name = "Kalem2",
                    Price = 200, 
                    Stock = 30, 
                    CreatedDate = DateTime.SpecifyKind(DateTime.Parse(DateTime.Now.ToString()), DateTimeKind.Local)
                } ,

                  new Product
                  {
                      ID = 4,
                      CategoryId = 1,
                      Name = "Kalem3",
                      Price = 600,
                      Stock = 60,
                      CreatedDate = DateTime.SpecifyKind(DateTime.Parse(DateTime.Now.ToString()), DateTimeKind.Local)
                  } ,

                   new Product
                   {
                       ID = 5,
                       CategoryId = 2,
                       Name = "Kitap1",
                       Price = 600,
                       Stock = 60,
                       CreatedDate = DateTime.SpecifyKind(DateTime.Parse(DateTime.Now.ToString()), DateTimeKind.Local)

        } ,

                    new Product
                    {
                        ID = 6,
                        CategoryId = 2,
                        Name = "Kitap2",
                        Price = 6060,
                        Stock = 360,
                        CreatedDate = DateTime.SpecifyKind(DateTime.Parse(DateTime.Now.ToString()), DateTimeKind.Local)

                    });
        }
    }
}
