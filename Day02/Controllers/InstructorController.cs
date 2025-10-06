using Day02.Data;
using Day02.Repository.Interfaces;
using Day02.Services.Interfaces;
using Day02.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day02.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IImageService _imageService;

        public InstructorController(IInstructorRepository instructorRepository, 
                                    IDepartmentRepository departmentRepository, 
                                    ICourseRepository courseRepository,
                                    IImageService imageService)
        {
            _instructorRepository = instructorRepository;
            _departmentRepository = departmentRepository;
            _courseRepository = courseRepository;
            _imageService = imageService;
        }
        public IActionResult Index()
        {
            //var instuctors = _context.Instructors.AsNoTracking().ToList();
            var instuctors = _instructorRepository.GetAll();
            return View(instuctors);
        }

        public IActionResult Details(int id)
        {
            //var instructor = _context.Instructors
            //                        .Include(i => i.Department)
            //                        .Include(i => i.Course)
            //                        .FirstOrDefault(i => i.Id == id);

            var includes = new string[] { "Department", "Course" };
            var instructor = _instructorRepository.GetAllWithInclude(includes).FirstOrDefault(i => i.Id == id);
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
            //instructorVM.Departments = _context.Departments.AsNoTracking().ToList();
            instructorVM.Departments = _departmentRepository.GetAll();
            //instructorVM.Courses = _context.Courses.AsNoTracking().ToList();
            instructorVM.Courses = _courseRepository.GetAll();
            return View(instructorVM);
        }

        
        public async Task<IActionResult> CreateAsync(CreateInstructorViewModel instructorVM)
        {
            

            if (instructorVM.Name is null || instructorVM.Address is null || instructorVM.Salary <= 0 || instructorVM.DepartmentId <= 0 || instructorVM.CourseId <= 0)
            {
                //TempData["Error"] = "Invalid Data";
                return RedirectToAction("CreateInstructor", instructorVM);
            }
            var instructor = new Models.Instructor()
            {
                Name = instructorVM.Name,
                ImageUrl = await _imageService.AddImageAsync(instructorVM.ImageUrl),
                Salary = instructorVM.Salary,
                Address = instructorVM.Address,
                DepartmentId = instructorVM.DepartmentId,
                CourseId = instructorVM.CourseId
            };
            //_context.Instructors.Add(instructor);
            _instructorRepository.Add(instructor);
            //_context.SaveChanges();
            _instructorRepository.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            // collect data from database
            //var instructor = _context.Instructors.AsNoTracking().FirstOrDefault(i => i.Id == id);
            var instructor = _instructorRepository.GetById(id);
            if (instructor == null)
                return NotFound();
            var instructorVM = new EditInstructorViewModel()
            {
                Id = instructor.Id,
                Name = instructor.Name,
                //ImageUrl = instructor.ImageUrl,
                Salary = instructor.Salary,
                Address = instructor.Address,
                DepartmentId = instructor.DepartmentId,
                CourseId = instructor.CourseId,
                //Departments = _context.Departments.AsNoTracking().ToList(),
                Departments = _departmentRepository.GetAll(),
                //Courses = _context.Courses.AsNoTracking().ToList()
                Courses = _courseRepository.GetAll()
            };
            return View(instructorVM);
        }

        [HttpPost]
        public async Task<IActionResult> saveEditAsync(EditInstructorViewModel instructorVM)
        {
            if (instructorVM.Name is null || instructorVM.Address is null || instructorVM.Salary <= 0 || instructorVM.DepartmentId <= 0 || instructorVM.CourseId <= 0)
            {
                //refill local lists
                //instructorVM.Departments = _context.Departments.AsNoTracking().ToList();
                instructorVM.Departments = _departmentRepository.GetAll();
                //instructorVM.Courses = _context.Courses.AsNoTracking().ToList();
                instructorVM.Courses = _courseRepository.GetAll();
                return RedirectToAction("Edit", instructorVM);
            }

            // get the original data
            //var instructor = _context.Instructors.FirstOrDefault(i => i.Id == instructorVM.Id);
            var instructor = _instructorRepository.GetById(instructorVM.Id);

            // mapping
            instructor.Name = instructorVM.Name;
            if (instructorVM.ImageUrl != null)
                instructor.ImageUrl = await _imageService.AddImageAsync(instructorVM.ImageUrl);
            //instructor.ImageUrl = instructorVM.ImageUrl;
            instructor.Salary = instructorVM.Salary;
            instructor.Address = instructorVM.Address;
            instructor.DepartmentId = instructorVM.DepartmentId;
            instructor.CourseId = instructorVM.CourseId;

            // save changes
            //_context.SaveChanges();
            //_instructorRepository.Update(instructor);
            _instructorRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
