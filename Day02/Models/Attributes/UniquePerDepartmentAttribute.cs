using Day02.Data;
using Day02.ViewModel.CourseViewModel;
using System.ComponentModel.DataAnnotations;

namespace Day02.Models.Attributes
{
    public class UniquePerDepartmentAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            AppDbContext _context = (AppDbContext)validationContext.GetService(typeof(AppDbContext))!;
            
            var courseName = value as string;
            var courseVM = validationContext.ObjectInstance as CreateCourseViewModel;
            var departmentId = courseVM!.DepartmentId;

            var existingCourse = _context.Courses
                .FirstOrDefault(c => c.Name == courseName && c.DepartmentId == departmentId);

            if (existingCourse is null)
                return ValidationResult.Success;
            return new ValidationResult("Course name must be unique within the same department.");
        }
    }
}
