using Day02.Data;
using Day02.Models;
using Day02.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day02.Repository.Implementation
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;
        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Course entity)
        {
            _context.Courses.Add(entity);
        }

        public void Delete(int id)
        {
            _context.Courses.Remove(GetById(id));
        }

        public List<Course> GetAll()
        {
            return _context.Courses.ToList();
        }

        public List<Course> GetAllWithIncludeDepartment()
        {
            return _context.Courses.Include(c => c.Department).ToList();
        }

        public Course GetById(int id)
        {
            return _context.Courses.FirstOrDefault(c => c.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Course entity)
        {
            Course courseFromDb = GetById(entity.Id);
            courseFromDb.Name = entity.Name;
            courseFromDb.Degree = entity.Degree;
            courseFromDb.MinDegree = entity.MinDegree;
            courseFromDb.Hours = entity.Hours;
            courseFromDb.DepartmentId = entity.DepartmentId;
        }

        
    }
}
