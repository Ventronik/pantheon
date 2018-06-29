using Microsoft.EntityFrameworkCore;

namespace PantheonApi.Models
{
    public class PantheonContext : DbContext
    {
        public PantheonContext(DbContextOptions<PantheonContext> options)
            : base(options)
        {
        }

        public DbSet<PantheonItem> PantheonItems { get; set; }
    }
}