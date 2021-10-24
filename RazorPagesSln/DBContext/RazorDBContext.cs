using Microsoft.EntityFrameworkCore;
using RazorPagesSln.Entity;
using RazorPagesSln.Entity.CustomerMngt;
using RazorPagesSln.Entity.Home;
using RazorPagesSln.Entity.Product;

namespace RazorPagesSln.DBContext
{
    public class RazorDBContext : DbContext
    {
        public RazorDBContext(DbContextOptions<RazorDBContext> options) : base(options)
        {
        }

        //Urls
        public DbSet<RedirectUrls> RedirectUrls { get; set; }

        //Login
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<LoginCredentials> LoginCredentials { get; set; }

        //Products
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        //Customer
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectSkill> ProjectSkills { get; set; }
        public DbSet<SDescription> SDescriptions { get; set; }
        public DbSet<PDescription> PDescriptions { get; set; }
    }
}
