using Day02.Models;
using Day02.Models.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Day02.ViewModel.CourseViewModel
{
    public class CreateCourseViewModel 
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [UniquePerDepartment]
        public required string Name { get; set; }
        [Range(50, 100)]
        public int Degree { get; set; }
        [Remote(action: "CheckMinDegree", controller: "Course", AdditionalFields = "Degree", ErrorMessage ="Min Degree must be less than Degree")]
        public int MinDegree { get; set; }
        [Remote(action: "CheckHoursDividedBy3", controller: "Course", ErrorMessage = "Hours must be divided by 3")]
        public int Hours { get; set; }

        public int DepartmentId { get; set; }
        public List<Department>? Departments { get; set; }

    }
}
