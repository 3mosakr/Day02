using Day02.Data;
using Day02.Models;
using Day02.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Day02.Repository.Implementation
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly AppDbContext _context;
        public InstructorRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Instructor entity)
        {
            _context.Instructors.Add(entity);
        }

        public void Delete(int id)
        {
            _context.Instructors.Remove(GetById(id));
        }

        public List<Instructor> GetAll()
        {
            return _context.Instructors.ToList();
        }

        public List<Instructor> GetAllWithInclude(string[] includes)
        {
            IQueryable<Instructor> query = _context.Instructors;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.ToList();
        }

        public Instructor GetById(int id)
        {
            return _context.Instructors.FirstOrDefault(i => i.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Instructor entity)
        {
            Instructor instructorFromDb = GetById(entity.Id);
            instructorFromDb.Name = entity.Name;
            instructorFromDb.Address = entity.Address;
            instructorFromDb.Salary = entity.Salary;
            instructorFromDb.ImageUrl = entity.ImageUrl;
            instructorFromDb.DepartmentId = entity.DepartmentId;
            instructorFromDb.CourseId = entity.CourseId;

        }
    }
}
