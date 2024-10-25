using Microsoft.EntityFrameworkCore;

namespace FakeBackend.Server.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
