using BLL.LibraryFileSystem.DTOs;
using BLL.LibraryFileSystem.Interfaces;
using DAL.Models;

namespace BLL.LibraryFileSystem.Services
{
    public class BookService : IFileSystemService<BookDTO>
    {
        private readonly ICrudHelper<BookDTO,Book> _crudHelper;

        public BookService(ICrudHelper<BookDTO, Book> crudHelper)
        {
            _crudHelper = crudHelper;
        }

        public void CreateDocument(BookDTO item)
        {
            _crudHelper.Create(item);
        }

        public void DeleteDocument(BookDTO item)
        {
            _crudHelper.Delete(item);
        }

        public BookDTO GetDocument(string type, int id)
        {
            return _crudHelper.Get(type,id);
        }

        public IEnumerable<BookDTO> GetAllDocuments(string type)
        {
            return _crudHelper.GetAll(type);
        }

    }
}
