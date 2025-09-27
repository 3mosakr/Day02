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

    }
}
