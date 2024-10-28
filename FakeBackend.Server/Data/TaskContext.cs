//using FakeBackend.Server.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;

//namespace FakeBackend.Server.Data
//{
//    public class TaskContext : DbContext
//    {
//        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }
//        public DbSet<DTask> Tasks { get; set; }
//    }
//}

using FakeBackend.Server.Models;
using Microsoft.EntityFrameworkCore;



namespace FakeBackend.Server.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        public DbSet<DTask> Tasks { get; set; }
        public DbSet<DTag> Tags { get; set; }  
    }
}

