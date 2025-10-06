using Day02.Models;
namespace Day02.Repository.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        List<Course> GetAllWithIncludeDepartment();
    }
}
