using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.LibraryFileSystem.Interfaces
{
    public interface IGenericService<T,D>  where T : class where D : class
    {
        void CreateDocument(D item);

        void DeleteDocument(D item);

        D GetDocument(string type, int id);

        IEnumerable<D> GetAllDocuments(string type);
    }
}
