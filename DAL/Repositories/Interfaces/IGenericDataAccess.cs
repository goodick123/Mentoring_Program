namespace DAL.Repositories.Interfaces
{
    public interface IGenericDataAccess<T>
    {
        void Create(T item);

        void Delete(T item);

        T Get(string type, int id);

        IEnumerable<T> GetAll(string type);
    }
}
