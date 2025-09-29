using Day02.Data;
using Day02.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day02.Controllers
{
    public class InstructorController : Controller
    {
        private readonly AppDbContext _context;
        public InstructorController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var instuctors = _context.Instructors.AsNoTracking().ToList();
            return View(instuctors);
        }

        public IActionResult Details(int id)
        {
            var instructor = _context.Instructors
                                    .Include(i => i.Department)
                                    .Include(i => i.Course)
                                    .FirstOrDefault(i => i.Id == id);
            if (instructor == null)
                return NotFound();

            // mapping
            InstructorDetailsViewModel instructorVM = new InstructorDetailsViewModel()
            {
                Name = instructor.Name,
                Address = instructor.Address,
                img = instructor.ImageUrl,
                Department = instructor.Department.Name,
                Course = instructor.Course.Name,
                Salary = instructor.Salary
            };

            return View(instructorVM);
        }

        
        public IActionResult CreateInstructor(CreateInstructorViewModel? instructorVM)
        {
            instructorVM.Departments = _context.Departments.AsNoTracking().ToList();
            instructorVM.Courses = _context.Courses.AsNoTracking().ToList();
            return View(instructorVM);
        }

        
        public IActionResult Create(CreateInstructorViewModel instructorVM)
        {
            if (instructorVM.Name is null || instructorVM.Address is null || instructorVM.ImageUrl is null || instructorVM.Salary <= 0 || instructorVM.DepartmentId <= 0 || instructorVM.CourseId <= 0)
            {
                //TempData["Error"] = "Invalid Data";
                return RedirectToAction("CreateInstructor", instructorVM);
            }
            var instructor = new Models.Instructor()
            {
                Name = instructorVM.Name,
                ImageUrl = instructorVM.ImageUrl,
                Salary = instructorVM.Salary,
                Address = instructorVM.Address,
                DepartmentId = instructorVM.DepartmentId,
                CourseId = instructorVM.CourseId
            };
            _context.Instructors.Add(instructor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            // collect data from database
            var instructor = _context.Instructors.AsNoTracking().FirstOrDefault(i => i.Id == id);
            if (instructor == null)
                return NotFound();
            var instructorVM = new EditInstructorViewModel()
            {
                Id = instructor.Id,
                Name = instructor.Name,
                ImageUrl = instructor.ImageUrl,
                Salary = instructor.Salary,
                Address = instructor.Address,
                DepartmentId = instructor.DepartmentId,
                CourseId = instructor.CourseId,
                Departments = _context.Departments.AsNoTracking().ToList(),
                Courses = _context.Courses.AsNoTracking().ToList()
            };
            return View(instructorVM);
        }

        [HttpPost]
        public IActionResult saveEdit(EditInstructorViewModel instructorVM)
        {
            if (instructorVM.Name is null || instructorVM.Address is null || instructorVM.ImageUrl is null || instructorVM.Salary <= 0 || instructorVM.DepartmentId <= 0 || instructorVM.CourseId <= 0)
            {
                //refill local lists
                instructorVM.Departments = _context.Departments.AsNoTracking().ToList();
                instructorVM.Courses = _context.Courses.AsNoTracking().ToList();
                return RedirectToAction("Edit", instructorVM);
            }

            // get the original data
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == instructorVM.Id);

            // mapping
            instructor.Name = instructorVM.Name;
            instructor.ImageUrl = instructorVM.ImageUrl;
            instructor.Salary = instructorVM.Salary;
            instructor.Address = instructorVM.Address;
            instructor.DepartmentId = instructorVM.DepartmentId;
            instructor.CourseId = instructorVM.CourseId;

            // save changes
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
