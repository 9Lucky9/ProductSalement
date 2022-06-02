namespace ProductSalement.Repository
{
    public interface IRepository<T> where T : class
    {
        public void Create(T item);
        public T Get(int id);
        public void Update(T item);
        public void Remove(int id);

    }
}
