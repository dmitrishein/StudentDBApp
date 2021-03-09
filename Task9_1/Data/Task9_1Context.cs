using Microsoft.EntityFrameworkCore;

namespace Task9_1.Data
{
    public class Task9_1Context : DbContext
    {
        public Task9_1Context (DbContextOptions<Task9_1Context> options)
            : base(options)
        {
        }
        public DbSet<Task9_1.Models.Student> Student { get; set; }

        public DbSet<Task9_1.Models.Group> Group { get; set; }

        public DbSet<Task9_1.Models.Course> Course { get; set; }
    }
}
