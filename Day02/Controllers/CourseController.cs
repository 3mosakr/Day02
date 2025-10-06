using AutoMapper;
using Day02.Data;
using Day02.Models;
using Day02.Repository.Interfaces;
using Day02.Validations;
using Day02.ViewModel.CourseViewModel;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day02.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public readonly IValidator<CreateCourseViewModel> _createCourseValidator;

        public CourseController(ICourseRepository courseRepository,
                                IDepartmentRepository departmentRepository,
                                IMapper mapper,
                                IValidator<CreateCourseViewModel> createCourseValidator)
        {
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _createCourseValidator = createCourseValidator;
        }

        public IActionResult Index(int page = 1, int size = 10)
        {
            //var courses = _context.Courses.Include(c => c.Department).AsNoTracking().ToList();
            //var courses = _courseRepository.GetAllWithIncludeDepartment();
            var courses = _courseRepository.GetAllPagination(page, size);
            PaginatCourseViewModel paginatCourseVM = new PaginatCourseViewModel()
            {
                CurrentPage = page,
                PageSize = size,
                TotalCount = _courseRepository.GetAll().Count,
                Courses = courses
            };
            return View("Index", paginatCourseVM);
        }

        public IActionResult New()
        {
            var createCourseViewModel = new CreateCourseViewModel()
            {
                Name = string.Empty,
                //Departments = _context.Departments.AsNoTracking().ToList()
                Departments = _departmentRepository.GetAll()
            };
            return View("New", createCourseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult saveNew(CreateCourseViewModel createCourseVM)
        {
            //if (createCourseVM.Name is null || createCourseVM.Degree == 0 || createCourseVM.MinDegree == 0 || createCourseVM.DepartmentId == 0)
            //    return RedirectToAction("New", createCourseVM);
            //if (!ModelState.IsValid)
            //{
            //    //createCourseVM.Departments = _context.Departments.AsNoTracking().ToList();
            //    createCourseVM.Departments = _departmentRepository.GetAll();
            //    return View("New", createCourseVM);
            //}
            var validationResult = _createCourseValidator.Validate(createCourseVM);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                //createCourseVM.Departments = _context.Departments.AsNoTracking().ToList();
                createCourseVM.Departments = _departmentRepository.GetAll();
                return View("New", createCourseVM);
            }
            // mapping
            //var Course = new Course()
            //{
            //    Name = createCourseVM.Name,
            //    Degree = createCourseVM.Degree,
            //    MinDegree = createCourseVM.MinDegree,
            //    Hours = createCourseVM.Hours,
            //    DepartmentId = createCourseVM.DepartmentId,
            //};
            // Using AutoMapper
            var Course = _mapper.Map<Course>(createCourseVM);

            try
            {
                // add to courses and save changes
                //_context.Courses.Add(Course);
                _courseRepository.Add(Course);
                //_context.SaveChanges();
                _courseRepository.Save();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ServerError", ex.Message);
                //createCourseVM.Departments = _context.Departments.AsNoTracking().ToList();
                createCourseVM.Departments = _departmentRepository.GetAll();
                return View("New", createCourseVM);
            }

            // redirect to index
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            // collect data from database
            //var course = _context.Courses.AsNoTracking().FirstOrDefault(c => c.Id == id);
            var course = _courseRepository.GetById(id);
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
                //Departments = _context.Departments.AsNoTracking().ToList()
                Departments = _departmentRepository.GetAll()
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
                //editCourseVM.Departments = _context.Departments.AsNoTracking().ToList();
                editCourseVM.Departments = _departmentRepository.GetAll();
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
            //_context.Courses.Update(course);
            _courseRepository.Update(course);
            //_context.SaveChanges();
            _courseRepository.Save();
            // redirect to index
            return RedirectToAction("index");
        }

        public IActionResult Search (string searchString = "")
        {
            //var courses = _context.Courses.Include(c => c.Department)
            //    .Where(c => c.Name.Contains(term)).AsNoTracking().ToList();
            var courses = _courseRepository.GetAllWithIncludeDepartment()
                .Where(c => c.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();
            return View("Index", courses);
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
