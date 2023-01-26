namespace BLL.LibraryFileSystem.Interfaces
{
    public interface IFileSystemService<T>
    {
        void CreateDocument(T item);

        void DeleteDocument(T item);

        T GetDocument(string type, int id);

        IEnumerable<T> GetAllDocuments(string type);
    }
}
