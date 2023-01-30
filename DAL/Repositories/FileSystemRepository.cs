using DAL.Models.Base;
using DAL.Repositories.Interfaces;
using System.Text.Json;

namespace DAL.Repositories
{
    public class FileSystemRepository<T> : IGenericDataAccess<T> where T : BaseFileModel
    {
        private readonly string _directoryPath = Directory.GetCurrentDirectory();

        public void Create(T item)
        {
            var path = Path.Combine(_directoryPath, $"{typeof(T).Name}_#{item.Id}.json");

            if (File.Exists(path))
            {
                return;
            }

            using var jsonFileStream = new FileStream(path, FileMode.OpenOrCreate);
            JsonSerializer.Serialize(jsonFileStream, item);
        }

        public void Delete(T item)
        {
            var path = Path.Combine(_directoryPath, $"{typeof(T).Name}_#{item.Id}.json");
            File.Delete(path);
        }

        public T Get(string type, int id)
        {
            var path = Path.Combine(_directoryPath, $"{type}_#{id}.json");

            if (!File.Exists(path))
            {
                throw new DirectoryNotFoundException("id is wrong.");
            }

            using var jsonFileStream = new FileStream(path, FileMode.OpenOrCreate);
            var fileObject = JsonSerializer.Deserialize<T>(jsonFileStream);

            return fileObject;
        }

        public IEnumerable<T> GetAll(string type)
        {
            var fileNames = Directory.GetFiles(_directoryPath).Where(x => x.Contains($"{type}_#"));

            var result = new List<T>();

            foreach (var fileName in fileNames)
            {
                var path = Path.Combine(_directoryPath, fileName.Trim());

                using var jsonFileStream = new FileStream(path, FileMode.OpenOrCreate);
                var fileObject = JsonSerializer.Deserialize<T>(jsonFileStream);

                result.Add(fileObject);
            }

            return result;
        }
    }
}
