using AutoMapper;
using BLL.LibraryFileSystem.Interfaces;
using BLL.LibraryFileSystem.Options;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BLL.LibraryFileSystem.Helpers
{
    public class CrudHelper<T,D> : ICrudHelper<T,D>
    {
        private readonly IGenericDataAccess<D> _genericDataAccess;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly CachingExpirationTimeOptions _expirationTimeOptions;
        public CrudHelper(IGenericDataAccess<D> genericDataAccess,
            IMapper mapper,
            IMemoryCache cache,
            IOptions<CachingExpirationTimeOptions> options)
        {
            _genericDataAccess = genericDataAccess;
            _mapper = mapper;
            _cache = cache;
            _expirationTimeOptions = options.Value;
        }
        public void Create(T item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var mappedBook = _mapper.Map<D>(item);

            _genericDataAccess.Create(mappedBook);
        }

        public void Delete(T item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var mappedBook = _mapper.Map<D>(item);

            _genericDataAccess.Delete(mappedBook);
        }

        public T Get(string type, int id)
        {
            if (string.IsNullOrEmpty(type) || id < 0)
            {
                throw new ArgumentException("Check type or id value");
            }

            var rawDocument = SearchItemInCache(type, id);

            return _mapper.Map<T>(rawDocument);
        }

        public IEnumerable<T> GetAll(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentException("Check type value");
            }

            var rawResult = _genericDataAccess.GetAll(type);

            return _mapper.Map<List<T>>(rawResult);
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
