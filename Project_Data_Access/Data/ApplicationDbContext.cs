using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project_Model;

namespace Project_Data_Access.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<catagory> Categries { get; set; }
        public DbSet<Covertype> Covertypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser>ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set;}
        public DbSet<Order> Orders { get; set; }
        public DbSet<shopingcart> Shopingcarts { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        //public DbSet<Country> Cotarys { get; set; }
        //public DbSet<city> Citys { get; set; }
    }

}