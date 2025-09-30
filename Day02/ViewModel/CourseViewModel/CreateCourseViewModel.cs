using Day02.Models;

namespace Day02.ViewModel.CourseViewModel
{
    public class CreateCourseViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public int Hours { get; set; }

        public int DepartmentId { get; set; }
        public List<Department>? Departments { get; set; }

    }
}
