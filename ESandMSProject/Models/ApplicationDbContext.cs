using ESandMSProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ESandMSProject.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Scheduling> Schedulings { get; set; }
        public DbSet<Login> Logins { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Scheduling>().HasOne(x => x.Student).WithMany(x => x.Schedulings).OnDelete(DeleteBehavior.ClientSetNull);
            base.OnModelCreating(modelBuilder);
        }
    }
}
