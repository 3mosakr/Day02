using Day02.Data;
using Day02.Models;
using Day02.ViewModel.CourseViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day02.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var courses = _context.Courses.Include(c => c.Department).AsNoTracking().ToList();
            return View("Index", courses);
        }

        public IActionResult New()
        {
            var createCourseViewModel = new CreateCourseViewModel()
            {
                Name = string.Empty,
                Departments = _context.Departments.AsNoTracking().ToList()
            };
            return View("New", createCourseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult saveNew(CreateCourseViewModel createCourseVM)
        {
            //if (createCourseVM.Name is null || createCourseVM.Degree == 0 || createCourseVM.MinDegree == 0 || createCourseVM.DepartmentId == 0)
            //    return RedirectToAction("New", createCourseVM);
            if (!ModelState.IsValid)
            {
                createCourseVM.Departments = _context.Departments.AsNoTracking().ToList();
                return View("New", createCourseVM);
            }
            // mapping
            var Course = new Course()
            {
                Name = createCourseVM.Name,
                Degree = createCourseVM.Degree,
                MinDegree = createCourseVM.MinDegree,
                Hours = createCourseVM.Hours,
                DepartmentId = createCourseVM.DepartmentId,
            };
            try
            {
                // add to courses and save changes
                _context.Courses.Add(Course);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ServerError", ex.Message);
                createCourseVM.Departments = _context.Departments.AsNoTracking().ToList();
                return View("New", createCourseVM);
            }

            // redirect to index
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            // collect data from database
            var course = _context.Courses.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (course is null)
                return NotFound();
            // mapping
            var editCourseViewModel = new CreateCourseViewModel()
            {
                Id = course.Id,
                Name = course.Name,
                Degree = course.Degree,
                MinDegree = course.MinDegree,
                Hours = course.MinDegree,
                DepartmentId = course.DepartmentId,
                Departments = _context.Departments.AsNoTracking().ToList()
            };

            return View("Edit", editCourseViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult saveEdit(CreateCourseViewModel editCourseVM)
        {
            //if (editCourseVM.Name is null || editCourseVM.Degree == 0 || editCourseVM.MinDegree == 0 || editCourseVM.DepartmentId == 0)
            //    return RedirectToAction("Edit", editCourseVM);

            if (!ModelState.IsValid)
            {
                editCourseVM.Departments = _context.Departments.AsNoTracking().ToList();
                return View("Edit", editCourseVM);
            }
            // mapping new data
            var course = new Course()
            {
                Id = editCourseVM.Id,
                Name = editCourseVM.Name,
                Degree = editCourseVM.Degree,
                MinDegree = editCourseVM.MinDegree,
                Hours = editCourseVM.Hours,
                DepartmentId = editCourseVM.DepartmentId,
            };
            // update to courses and save changes
            _context.Courses.Update(course);
            _context.SaveChanges();
            // redirect to index
            return RedirectToAction("index");
        }

        #region Remote validation
        public IActionResult CheckMinDegree(int MinDegree, int Degree)
        {
            if (MinDegree > Degree)
                return Json(false);
            return Json(true);
        }

        public IActionResult CheckHoursDividedBy3(int Hours)
        {
            if (Hours % 3 != 0)
                return Json(false);
            return Json(true);
        }
        #endregion
    }
}
