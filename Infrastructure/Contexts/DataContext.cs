using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<CourseContentEntity> CourseContents { get; set; }
        public DbSet<CourseIncludesEntity> CourseIncludes { get; set; }
        public DbSet<ProgramDetailsEntity> ProgramDetails { get; set; }
        public DbSet<SavedCoursesEntity> SavedCourses { get; set; }
  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //entity mappings
            modelBuilder.Entity<CategoryEntity>().ToTable("Categories");
            modelBuilder.Entity<CourseEntity>().ToTable("Courses");
            modelBuilder.Entity<CourseContentEntity>().ToTable("CourseContents");
            modelBuilder.Entity<CourseIncludesEntity>().ToTable("CourseIncludes");
            modelBuilder.Entity<ProgramDetailsEntity>().ToTable("ProgramDetails");
            modelBuilder.Entity<SavedCoursesEntity>().ToTable("SavedCourses");

            base.OnModelCreating(modelBuilder);
        }
    }
}