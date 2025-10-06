using Day02.Models;

namespace Day02.Repository.Interfaces
{
    public interface IInstructorRepository : IBaseRepository<Instructor>
    {
        List<Instructor> GetAllWithInclude(string[] includes);
    }
}
