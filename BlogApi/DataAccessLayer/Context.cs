using Microsoft.EntityFrameworkCore;

namespace BlogApi.DataAccessLayer
{
    public class Context:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-N6O4A6E;database=BlogApiDb;integrated security=true;TrustServerCertificate=True;");
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
