
namespace BLL.LibraryFileSystem.Interfaces
{
    public interface ICrudHelper<T,D>
    {
        public void Create(T item);

        public void Delete(T item);

        public T Get(string type, int id);

        public IEnumerable<T> GetAll(string type);
    }
}
