using Day02.Data;
using Day02.Models;
using Day02.Repository.Interfaces;

namespace Day02.Repository.Implementation
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Department entity)
        {
            _context.Departments.Add(entity);
        }

        public void Delete(int id)
        {
            _context.Departments.Remove(GetById(id));
        }

        public List<Department> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return _context.Departments.FirstOrDefault(d => d.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Department entity)
        {
            Department departmentFromDb = GetById(entity.Id);
            departmentFromDb.Name = entity.Name;
            departmentFromDb.Manager = entity.Manager;
            
        }
    }
}
