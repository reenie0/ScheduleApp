using Microsoft.EntityFrameworkCore;
using ScheduleApp.Models;

namespace ScheduleApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>()
                .HasOne(r => r. Schedule)
                .WithMany(s => s.Request)
                .HasForeignKey(r => r.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

        }
    }
}


