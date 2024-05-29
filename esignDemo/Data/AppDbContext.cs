using Microsoft.EntityFrameworkCore;
using esignDemo.Models;

namespace esignDemo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=esignDemo.db");
        }
    }
}