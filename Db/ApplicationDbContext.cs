using Microsoft.EntityFrameworkCore;
using Record.Model;

namespace Record.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) {
        
        }

        public DbSet<Employees> Employees { get; set; }
    }
}
