using _01.APIDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace _01.APIDemo.Context
{
    public class APIContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseSqlServer("Server=DESKTOP-ASRSF7U\\SQLEXPRESS;Initial Catalog=APIDemo;Integrated security=True;Trustservercertificate=True;");
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
