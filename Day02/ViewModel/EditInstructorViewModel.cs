using Day02.Models;

namespace Day02.ViewModel
{
    public class EditInstructorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? ImageUrl { get; set; }
        //public string? ImageUrl { get; set; }
        public int Salary { get; set; }
        public string? Address { get; set; }

        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public List<Department> Departments { get; set; } = new List<Department>();
        public List<Course> Courses { get; set; } = new List<Course>();
    }
}
