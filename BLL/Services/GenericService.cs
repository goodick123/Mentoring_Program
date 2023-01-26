using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.LibraryFileSystem.Services
{
    public class BookService : IFileSystemService<BookDTO>
    {
        private readonly IGenericDataAccess<Book> _genericDataAccess;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly CachingExpirationTimeOptions _expirationTimeOptions;

        public BookService(
            IGenericDataAccess<Book> genericDataAccess,
            IMapper mapper,
            IMemoryCache cache,
            IOptions<CachingExpirationTimeOptions> options)
        {
            _genericDataAccess = genericDataAccess;
            _mapper = mapper;
            _cache = cache;
            _expirationTimeOptions = options.Value;
        }

        public void CreateDocument(BookDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var mappedBook = _mapper.Map<Book>(item);

            _genericDataAccess.Create(mappedBook);
        }

        public void DeleteDocument(BookDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var mappedBook = _mapper.Map<Book>(item);

            _genericDataAccess.Delete(mappedBook);
        }

        public BookDTO GetDocument(string type, int id)
        {
            if (string.IsNullOrEmpty(type) || id < 0)
            {
                throw new ArgumentException("Check type or id value");
            }

            var rawDocument = SearchItemInCache(type, id);

            return _mapper.Map<BookDTO>(rawDocument);
        }

        public IEnumerable<BookDTO> GetAllDocuments(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Check type value");
            }

            var rawResult = _genericDataAccess.GetAll(type);

            return _mapper.Map<List<BookDTO>>(rawResult);
        }

        private object SearchItemInCache(string type, int id)
        {
            var name = $"{type}_#{id}";
            var fileContents = _cache.Get(name);

            if (fileContents == null)
            {
                fileContents = _genericDataAccess.Get(type, id);

                _cache.Set(name, fileContents, TimeSpan.FromMinutes(_expirationTimeOptions.Book));
            }

            return fileContents;
        }
    }
}
