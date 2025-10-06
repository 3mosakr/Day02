namespace Day02.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        void Save();

    }
}
