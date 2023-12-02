namespace werd.Repository
{
     public interface IRepository<T>
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public int Add(T entity);
        public int Update(T entity);
        public bool Delete(string[] ids);
    }
}
