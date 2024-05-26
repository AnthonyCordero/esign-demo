using Microsoft.EntityFrameworkCore;
using esign-demo.Models;

namespace esign-demo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("");  
            // Pending define bd context
        }
    }
}
                  // IMPORTANT

         // One completed this section ejecute

         // dotnet ef migrations add InitialCreate
         // dotnet ef database update

         // in the console