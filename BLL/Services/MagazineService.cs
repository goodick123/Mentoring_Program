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
    public class MagazineService : IFileSystemService<MagazineDTO>
    {
        private readonly IGenericDataAccess<Magazine> _genericDataAccess;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly CachingExpirationTimeOptions _expirationTimeOptions;

        public MagazineService(
            IGenericDataAccess<Magazine> genericDataAccess, 
            IMapper mapper, 
            IMemoryCache cache, 
            IOptions<CachingExpirationTimeOptions> options)
        {
            _genericDataAccess = genericDataAccess;
            _mapper = mapper;
            _cache = cache;
            _expirationTimeOptions = options.Value;
        }

        public void CreateDocument(MagazineDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var mappedMagazine = _mapper.Map<Magazine>(item);

            _genericDataAccess.Create(mappedMagazine);
        }

        public void DeleteDocument(MagazineDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var mappedMagazine = _mapper.Map<Magazine>(item);

            _genericDataAccess.Delete(mappedMagazine);
        }

        public MagazineDTO GetDocument(string type, int id)
        {
            if (string.IsNullOrEmpty(type) || id < 0)
            {
                throw new ArgumentException("Check type or id value");
            }

            var rawDocument = SearchItemInCache(type, id);

            return _mapper.Map<MagazineDTO>(rawDocument);
        }

        public IEnumerable<MagazineDTO> GetAllDocuments(string type)
        {
            ArgumentNullException.ThrowIfNull(type);

            var rawResult = _genericDataAccess.GetAll(type);

            return _mapper.Map<List<MagazineDTO>>(rawResult);
        }

        private object SearchItemInCache(string type, int id)
        {
            var name = $"{type}_#{id}";
            var fileContents = _cache.Get(name);

            if (fileContents == null)
            {
                // Fetch the file contents.  
                fileContents = _genericDataAccess.Get(type, id);

                _cache.Set(name, fileContents, TimeSpan.FromMinutes(_expirationTimeOptions.Magazine));
            }

            return fileContents;
        }
    }
}
