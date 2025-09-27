using System.ComponentModel.DataAnnotations;

namespace Day02.Models
{
    public class CrsResult
    {
        public int Degree { get; set; }
        public int TraineeId { get; set; }
        public Trainee? Trainee { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }

    }
}
