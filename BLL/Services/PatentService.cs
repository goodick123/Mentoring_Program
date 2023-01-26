﻿using AutoMapper;
using BLL.LibraryFileSystem.DTOs;
using BLL.LibraryFileSystem.Interfaces;
using BLL.LibraryFileSystem.Options;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BLL.LibraryFileSystem.Services
{
    public class PatentService : IFileSystemService<PatentDTO>
    {
        private readonly IGenericDataAccess<Patent> _genericDataAccess;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly CachingExpirationTimeOptions _expirationTimeOptions;

        public PatentService(
            IGenericDataAccess<Patent> genericDataAccess, 
            IMapper mapper, 
            IMemoryCache cache, 
            IOptions<CachingExpirationTimeOptions> options)
        {
            _genericDataAccess = genericDataAccess;
            _mapper = mapper;
            _cache = cache;
            _expirationTimeOptions = options.Value;
        }
        public void CreateDocument(PatentDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var mappedPatent = _mapper.Map<Patent>(item);

            _genericDataAccess.Create(mappedPatent);
        }

        public void DeleteDocument(PatentDTO item)
        {
            ArgumentNullException.ThrowIfNull(item);

            var mappedPatent = _mapper.Map<Patent>(item);

            _genericDataAccess.Delete(mappedPatent);
        }

        public PatentDTO GetDocument(string type, int id)
        {
            if (string.IsNullOrEmpty(type) || id < 0)
            {
                throw new ArgumentException("Check type or id value");
            }

            var rawDocument = SearchItemInCache(type, id);

            return _mapper.Map<PatentDTO>(rawDocument);
        }

        public IEnumerable<PatentDTO> GetAllDocuments(string type)
        {
            ArgumentNullException.ThrowIfNull(type);

            var rawResult = _genericDataAccess.GetAll(type);

            return _mapper.Map<List<PatentDTO>>(rawResult);
        }

        private object SearchItemInCache(string type, int id)
        {
            var name = $"{type}_#{id}";
            var fileContents = _cache.Get(name);

            if (fileContents == null)
            {
                // Fetch the file contents.  
                fileContents = _genericDataAccess.Get(type, id);

                _cache.Set(name, fileContents, TimeSpan.FromMinutes(_expirationTimeOptions.Patent));
            }

            return fileContents;
        }
    }
}
