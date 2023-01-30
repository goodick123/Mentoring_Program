using BLL.LibraryFileSystem.DTOs;
using BLL.LibraryFileSystem.Interfaces;
using DAL.Models;

namespace BLL.LibraryFileSystem.Services
{
    public class PatentService : IFileSystemService<PatentDTO>
    {
        private readonly ICrudHelper<PatentDTO, Patent> _crudHelper;

        public PatentService(ICrudHelper<PatentDTO, Patent> crudHelper)
        {
            _crudHelper = crudHelper;
        }
        public void CreateDocument(PatentDTO item)
        {
            _crudHelper.Create(item);
        }

        public void DeleteDocument(PatentDTO item)
        {
            _crudHelper.Delete(item);
        }

        public PatentDTO GetDocument(string type, int id)
        {
            return _crudHelper.Get(type, id);
        }

        public IEnumerable<PatentDTO> GetAllDocuments(string type)
        {
            return _crudHelper.GetAll(type);
        }
    }
}
