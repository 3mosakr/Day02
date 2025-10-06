using Day02.Models;

namespace Day02.ViewModel.CourseViewModel
{
    public class PaginatCourseViewModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public List<Course> Courses { get; set; } = new List<Course>();

    }
}
