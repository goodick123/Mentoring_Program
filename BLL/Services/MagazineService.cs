using BLL.LibraryFileSystem.DTOs;
using BLL.LibraryFileSystem.Interfaces;
using DAL.Models;

namespace BLL.LibraryFileSystem.Services
{
    public class MagazineService : IFileSystemService<MagazineDTO>
    {
        private readonly ICrudHelper<MagazineDTO, Magazine> _crudHelper;

        public MagazineService(ICrudHelper<MagazineDTO, Magazine> crudHelper)
        {
            _crudHelper = crudHelper;
        }

        public void CreateDocument(MagazineDTO item)
        {
            _crudHelper.Create(item);
        }

        public void DeleteDocument(MagazineDTO item)
        {
            _crudHelper.Delete(item);
        }

        public MagazineDTO GetDocument(string type, int id)
        {
            return _crudHelper.Get(type, id);
        }

        public IEnumerable<MagazineDTO> GetAllDocuments(string type)
        {
            return _crudHelper.GetAll(type);
        }
    }
}
