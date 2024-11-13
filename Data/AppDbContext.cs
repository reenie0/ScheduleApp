using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScheduleApp.Models;
using ScheduleApp.ViewModels;

namespace ScheduleApp.Data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<AppointmentRequest> AppointmentRequests { get; set; }
        public DbSet<LecturerSchedule> LecturerSchedules { get; set; }
        public DbSet<StudentSignupModel> StudentSignups { get; set; }
        public DbSet<Users> User { get; set; }
        public DbSet<LecturerSignupModel> LecturerSignupModels { get; set; }
        public DbSet<AvailableSlot> AvailableSlots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Apply Identity configurations

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Schedule)
                .WithMany(s => s.Request)
                .HasForeignKey(r => r.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            // Define the relationship between AvailableSlot and Lecturer
            modelBuilder.Entity<AvailableSlot>()
                .HasOne(a => a.Lecturer) // Navigation property in AvailableSlot
                .WithMany(l => l.AvailableSlots) // Navigation property in Lecturer
                .HasForeignKey(a => a.LecturerId) // Foreign key in AvailableSlot
                .OnDelete(DeleteBehavior.Restrict); // Optional: Prevent cascading deletes

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Student)
                .WithMany() // or .WithMany(s => s.Requests) if you want reverse navigation
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Restrict); // Specify delete behavior

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Lecturer)
                .WithMany() // or .WithMany(l => l.Requests) if you want reverse navigation
                .HasForeignKey(r => r.LecturerId)
                .OnDelete(DeleteBehavior.Restrict); // Specify delete behavior

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Schedule)
                .WithMany() // or .WithMany(s => s.Requests) if you want reverse navigation
                .HasForeignKey(r => r.ScheduleId)
                .OnDelete(DeleteBehavior.Restrict); // Specify delete behavior
        }

    }
    }

