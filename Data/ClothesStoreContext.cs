using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ClothesStore.Models;

namespace ClothesStore.Data
{
    public class ClothesStoreContext : IdentityDbContext<ApplicationUserz, Rolez, Guid>
    {
        public ClothesStoreContext(DbContextOptions<ClothesStoreContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
    }

}
