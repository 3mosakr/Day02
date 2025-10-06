using AutoMapper;
using Day02.Models;
using Day02.ViewModel.CourseViewModel;

namespace Day02.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateCourseViewModel to Course
            CreateMap<CreateCourseViewModel, Course>();
            
        }
    }
}
