using Microsoft.EntityFrameworkCore;

namespace FakeBackend.Server.Models
{
    public class FakeDBContext : DbContext
    {
#pragma warning disable IDE0290 // Use primary constructor
        public FakeDBContext(DbContextOptions<FakeDBContext> options) : base(options)
#pragma warning restore IDE0290 // Use primary constructor
        {
        }

        
        public DbSet<DTask> DTasks { get; set; }
    }
}
