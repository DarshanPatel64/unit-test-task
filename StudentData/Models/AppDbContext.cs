using Microsoft.EntityFrameworkCore;

namespace StudentData.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
                
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> students { get; set; }
    }
}
