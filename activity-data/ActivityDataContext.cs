using activity_model;
using Microsoft.EntityFrameworkCore;

namespace activity_data
{
    public class ActivityDataContext : DbContext
    {
        public ActivityDataContext(DbContextOptions<ActivityDataContext> options): base(options)
        {
            
        }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Value> Values { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder){

        }
    }
}