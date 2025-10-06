using Day02.Models;

namespace Day02.Repository.Interfaces
{
    public interface ITraineeRepository : IBaseRepository<Trainee>
    {
        Trainee GetTraineeWithCourses(int id);
    }
}
