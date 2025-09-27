using Day02.Data;
using Day02.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day02.Controllers
{
    public class TraineeController : Controller
    {
        private readonly AppDbContext _context;
        public TraineeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var traineeCourse = new TraineeCourseViewModel();
            traineeCourse.Courses = _context.Courses.ToList();
            traineeCourse.Trainees = _context.Trainees.ToList();


            return View(traineeCourse);
        }

        public IActionResult Result(int id, int courseId)
        {
            var traineeCourse = new TraineeCourseResultViewModel();
            var trainee = _context.Trainees
                                  .Include(t => t.CrsResults)
                                    .ThenInclude(cr => cr.Course)
                                  .FirstOrDefault(t => t.Id == id);
            if (trainee is not null)
            {
                traineeCourse.TraineeName = trainee.Name;
                traineeCourse.imgUrl = trainee.ImageUrl;

                var traineeResult = trainee.CrsResults.FirstOrDefault(cr => cr.CourseId == courseId);
                if (traineeResult is not null)
                {
                    traineeCourse.CourseName = traineeResult.Course.Name;
                    traineeCourse.CourseDegree = traineeResult.Degree;
                    traineeCourse.Status = (traineeResult.Degree >= traineeResult.Course.MinDegree ? "Pass" : "Fail");
                    traineeCourse.color = (traineeResult.Degree >= traineeResult.Course.MinDegree ? "green" : "red");
                }

            }
            return View(traineeCourse);
        }
    }
}
