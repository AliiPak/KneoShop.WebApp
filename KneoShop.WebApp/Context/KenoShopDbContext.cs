using KenoShop.WebApp.Entities;
using KenoShop.WebApp.Entities.Account;
using KenoShop.WebApp.Entities.BasicDefinitions;
using KenoShop.WebApp.Entities.Order;
using KenoShop.WebApp.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace KenoShop.WebApp.Context
{
    public class KenoShopDbContext : DbContext 
    {
        public KenoShopDbContext(DbContextOptions<KenoShopDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Product { get; set; }

        public DbSet<User> Users { get; set; }
        
        public DbSet<ProductCategory> ProductCategories { get; set; }
        
        public DbSet<ProductSelectedCategory> ProductSelectedCategoies { get; set; }
        
        public DbSet<Size> ProductSizes { get; set; }
        
        public DbSet<Color> ProductColors { get; set; }
        

        public DbSet<Brand> Brands { get; set; }
        
        public DbSet<ProductSelectedSize> ProductSelectedSizes { get; set; }
        
        public DbSet<ProductSelectedColor> ProductSelectedColors { get; set; }

        public DbSet<ProductGallery> ProductGalleries { get; set; }
        
        public DbSet<Inventory> Inventories { get; set; }
        
        public DbSet<Order> Orders { get; set; }
        
        public DbSet<OrderDetail> OrderDetails { get; set; }

        
        


    }
}
