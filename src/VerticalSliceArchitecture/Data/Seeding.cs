using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerticalSliceArchitecture.Entities;

namespace VerticalSliceArchitecture.Data
{
    public class Seeding
    {
        public static void SeedData(ApplicationDbContext context)
        {
            context.Products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product-1",
            });
            context.Products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product-2",
            });
            context.Products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = "Product-3",
            });

            context.SaveChanges();
        }
    }
}