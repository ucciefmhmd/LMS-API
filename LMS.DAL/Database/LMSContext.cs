using LMS.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Database
{
    public class LMSContext : DbContext
    {
        public LMSContext(DbContextOptions<LMSContext> opt):base(opt) { }

        public DbSet<Courses> Courses  { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<InstructorCourse> InstructorCourse { get; set; }
        public DbSet<Instructors> Instructors { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<StudentExam> StudentExam { get; set; }
        public DbSet<StudentQuestion> StudentQuestion { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<UserEvent> UserEvent { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEvent>().HasKey(a => new { a.user_ID, a.event_ID });
            modelBuilder.Entity<Group>()
               .HasOne(g => g.Students)
               .WithMany(s => s.Group)
               .HasForeignKey(g => g.Std_ID)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Students>()
              .HasOne(s => s.Users)
              .WithOne()
              .HasForeignKey<Students>(s => s.userID)
              .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Instructors>()
              .HasOne(s => s.Users)
              .WithOne()
              .HasForeignKey<Instructors>(s => s.userID)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
