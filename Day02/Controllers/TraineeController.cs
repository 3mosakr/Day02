using Day02.Data;
using Day02.Repository.Interfaces;
using Day02.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day02.Controllers
{
    public class TraineeController : Controller
    {
        private readonly ITraineeRepository _traineeRepository;
        private readonly ICourseRepository _courseRepository;
        public TraineeController(ITraineeRepository traineeRepository, ICourseRepository courseRepository)
        {
            _traineeRepository = traineeRepository;
            _courseRepository = courseRepository;
        }
        public IActionResult Index()
        {
            var traineeCourse = new TraineeCourseViewModel();
            //traineeCourse.Courses = _context.Courses.ToList();
            traineeCourse.Courses = _courseRepository.GetAll();
            //traineeCourse.Trainees = _context.Trainees.ToList();
            traineeCourse.Trainees = _traineeRepository.GetAll();


            return View(traineeCourse);
        }

        [Route("Trainee/Result/{id}/{courseId}")]
        public IActionResult Result(int id, int courseId)
        {
            var traineeCourse = new TraineeCourseResultViewModel();
            //var trainee = _context.Trainees
            //                      .Include(t => t.CrsResults)
            //                        .ThenInclude(cr => cr.Course)
            //                      .FirstOrDefault(t => t.Id == id);
            var trainee = _traineeRepository.GetTraineeWithCourses(id);
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
