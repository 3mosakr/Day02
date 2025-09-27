namespace Day02.ViewModel
{
    public class TraineeCourseResultViewModel
    {
        public string TraineeName { get; set; }
        public string CourseName { get; set; } = "NOT Enrolled in this course";
        public string Status { get; set; } = "NOT Enrolled";
        public int? CourseDegree { get; set; } 
        public string color { get; set; } 
        public string? imgUrl { get; set; }

    }
}
