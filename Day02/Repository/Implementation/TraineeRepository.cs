using Day02.Data;
using Day02.Models;
using Day02.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Day02.Repository.Implementation
{
    public class TraineeRepository : ITraineeRepository
    {
        private readonly AppDbContext _context;
        public TraineeRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Trainee entity)
        {
            _context.Trainees.Add(entity);
        }

        public void Delete(int id)
        {
            _context.Trainees.Remove(GetById(id));
        }

        public List<Trainee> GetAll()
        {
            return _context.Trainees.ToList();
        }

        public Trainee GetById(int id)
        {
            return _context.Trainees.FirstOrDefault(t => t.Id == id);
        }

        public Trainee GetTraineeWithCourses(int id)
        {
            return _context.Trainees
                           .Include(t => t.CrsResults)
                                .ThenInclude(cr => cr.Course)
                           .FirstOrDefault(t => t.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Trainee entity)
        {
            Trainee traineeFromDb = GetById(entity.Id);
            traineeFromDb.Name = entity.Name;
            traineeFromDb.Address = entity.Address;
            traineeFromDb.ImageUrl = entity.ImageUrl;
            traineeFromDb.DepartmentId = entity.DepartmentId;

        }
    }
}
