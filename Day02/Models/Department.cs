namespace Day02.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Manager { get; set; }

        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
        public ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
