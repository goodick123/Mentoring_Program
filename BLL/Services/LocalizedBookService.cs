using BLL.LibraryFileSystem.DTOs;
using BLL.LibraryFileSystem.Interfaces;
using DAL.Models;

namespace BLL.LibraryFileSystem.Services
{
    public class LocalizedBookService : IFileSystemService<LocalizedBookDTO>
    {
        private readonly ICrudHelper<LocalizedBookDTO, LocalizedBook> _crudHelper;

        public LocalizedBookService(ICrudHelper<LocalizedBookDTO, LocalizedBook> crudHelper)
        {
            _crudHelper = crudHelper;
        }
        public void CreateDocument(LocalizedBookDTO item)
        {
            _crudHelper.Create(item);
        }

        public void DeleteDocument(LocalizedBookDTO item)
        {
            _crudHelper.Delete(item);
        }

        public LocalizedBookDTO GetDocument(string type, int id)
        {
            return _crudHelper.Get(type, id);
        }

        public IEnumerable<LocalizedBookDTO> GetAllDocuments(string type)
        {
           return _crudHelper.GetAll(type);
        }
    }
}
