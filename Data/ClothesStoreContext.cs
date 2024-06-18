using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClothesStore.Models;

namespace ClothesStore.Data
{
    public class ClothesStoreContext : DbContext
    {
        public ClothesStoreContext (DbContextOptions<ClothesStoreContext> options)
            : base(options)
        {
        }

        public DbSet<ClothesStore.Models.Product> Product { get; set; } = default!;
    }

}
