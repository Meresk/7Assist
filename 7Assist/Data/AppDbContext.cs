using _7Assist.Models;
using Microsoft.EntityFrameworkCore;

namespace _7Assist.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users_lw9_02 { get; set; }
    }
}
