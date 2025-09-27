using Day02.Models;
using Microsoft.EntityFrameworkCore;

namespace Day02.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CrsResult> CrsResults { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Models.CrsResult>()
                .HasKey(cr => new { cr.TraineeId, cr.CourseId });

            // seed data
            List<Department> departments = new()
            {
                new Department { Id = 1, Name = "C#", Manager = "Ahmed" },
                new Department { Id = 2, Name = "Java", Manager = "Mohamed" },
                new Department { Id = 3, Name = "Python", Manager = "Omar" },
            };
            List<Course> courses = new()
            {
                new Course { Id = 1, Name = "C# Basics", Degree = 100, MinDegree = 50, Hours = 40, DepartmentId = 1 },
                new Course { Id = 2, Name = "C# Advanced", Degree = 100, MinDegree = 50, Hours = 60, DepartmentId = 1 },
                new Course { Id = 3, Name = "Java Basics", Degree = 100, MinDegree = 50, Hours = 40, DepartmentId = 2 },
                new Course { Id = 4, Name = "Java Advanced", Degree = 100, MinDegree = 50, Hours = 60, DepartmentId = 2 },
                new Course { Id = 5, Name = "Python Basics", Degree = 100, MinDegree = 50, Hours = 40, DepartmentId = 3 },
                new Course { Id = 6, Name = "Python Advanced", Degree = 100, MinDegree = 50, Hours = 60, DepartmentId = 3 },
            };
            List<Instructor> instructors = new()
            {
                new Instructor { Id = 1, Name = "Ali", Address = "Cairo", Salary = 10000, ImageUrl = "1.jpeg", DepartmentId = 1, CourseId = 1 },
                new Instructor { Id = 2, Name = "Hassan", Address = "Giza", Salary = 12000, ImageUrl = "2.png", DepartmentId = 2, CourseId = 3 },
                new Instructor { Id = 3, Name = "Mahmoud", Address = "Alex", Salary = 15000, ImageUrl = "3.png", DepartmentId = 3, CourseId = 5 },
            };
            List<Trainee> trainees = new()
            {
                new Trainee { Id = 1, Name = "Taha", Address = "Cairo", ImageUrl = "1.jpeg", DepartmentId = 1 },
                new Trainee { Id = 2, Name = "Youssef", Address = "Giza", ImageUrl = "2.png", DepartmentId = 2 },
                new Trainee { Id = 3, Name = "Khaled", Address = "Alex", ImageUrl = "3.png", DepartmentId = 3 },
                new Trainee { Id = 4, Name = "Omar", Address = "Cairo", ImageUrl = "3.png", DepartmentId = 1 },
                new Trainee { Id = 5, Name = "Salma", Address = "Giza", ImageUrl = "2.png", DepartmentId = 2 },
            };
            List<CrsResult> crsResults = new()
            {
                new CrsResult { TraineeId = 1, CourseId = 1, Degree = 80 },
                new CrsResult { TraineeId = 1, CourseId = 2, Degree = 70 },
                new CrsResult { TraineeId = 2, CourseId = 3, Degree = 90 },
                new CrsResult { TraineeId = 2, CourseId = 4, Degree = 85 },
                new CrsResult { TraineeId = 3, CourseId = 5, Degree = 95 },
                new CrsResult { TraineeId = 3, CourseId = 6, Degree = 80 },
                new CrsResult { TraineeId = 4, CourseId = 1, Degree = 60 },
                new CrsResult { TraineeId = 4, CourseId = 2, Degree = 75 },
                new CrsResult { TraineeId = 5, CourseId = 3, Degree = 88 },
                new CrsResult { TraineeId = 5, CourseId = 4, Degree = 92 },
            };

            modelBuilder.Entity<Department>().HasData(departments);
            modelBuilder.Entity<Course>().HasData(courses);
            modelBuilder.Entity<Instructor>().HasData(instructors);
            modelBuilder.Entity<Trainee>().HasData(trainees);
            modelBuilder.Entity<CrsResult>().HasData(crsResults);

        }
    }
}
