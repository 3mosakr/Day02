namespace Day02.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int Salary { get; set; }
        public string? Address { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }

    }
}
