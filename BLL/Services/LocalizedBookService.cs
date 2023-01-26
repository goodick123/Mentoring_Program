using AutoMapper;
using BLL.LibraryFileSystem.DTOs;
using BLL.LibraryFileSystem.Interfaces;
using BLL.LibraryFileSystem.Options;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BLL.LibraryFileSystem.Services
{
    public class LocalizedBookService : IFileSystemService<LocalizedBookDTO>
    {
        private readonly IGenericDataAccess<LocalizedBook> _genericDataAccess;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly CachingExpirationTimeOptions _expirationTimeOptions;

        public LocalizedBookService(
            IGenericDataAccess<LocalizedBook> genericDataAccess, 
            IMapper mapper, 
            IMemoryCache cache,
            IOptions<CachingExpirationTimeOptions> options)
        {
            _genericDataAccess = genericDataAccess;
            _mapper = mapper;
            _cache = cache;
            _expirationTimeOptions = options.Value;
        }
        public void CreateDocument(LocalizedBookDTO item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "null");
            }

            var mappedLocalizedBook = _mapper.Map<LocalizedBook>(item);

            _genericDataAccess.Create(mappedLocalizedBook);
        }

        public void DeleteDocument(LocalizedBookDTO item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item), "null");
            }

            var mappedLocalizedBook = _mapper.Map<LocalizedBook>(item);

            _genericDataAccess.Delete(mappedLocalizedBook);
        }

        public LocalizedBookDTO GetDocument(string type, int id)
        {
            if (string.IsNullOrEmpty(type) || id < 0)
            {
                throw new ArgumentException("Check type or id value");
            }

            var rawDocument = SearchItemInCache(type, id);

            return _mapper.Map<LocalizedBookDTO>(rawDocument);
        }

        public IEnumerable<LocalizedBookDTO> GetAllDocuments(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Check type value");
            }

            var rawResult = _genericDataAccess.GetAll(type);

            return _mapper.Map<List<LocalizedBookDTO>>(rawResult);
        }

        private object SearchItemInCache(string type, int id)
        {
            var name = $"{type}_#{id}";
            var fileContents = _cache.Get(name);

            if (fileContents == null)
            {
                // Fetch the file contents.  
                fileContents = _genericDataAccess.Get(type, id);

                _cache.Set(name, fileContents, TimeSpan.FromMinutes(_expirationTimeOptions.LocalizedBook));
            }

            return fileContents;
        }
    }
}
