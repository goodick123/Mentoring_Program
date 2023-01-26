using BLL.LibraryFileSystem.DTOs;
using BLL.LibraryFileSystem.Interfaces;
using Newtonsoft.Json;
using OOP.Interfaces;

namespace OOP
{
    public class FileStorageController : IFileStorageController
    {
        private readonly IFileSystemService<BookDTO> _bookService;
        private readonly IFileSystemService<LocalizedBookDTO> _localizedBookService;
        private readonly IFileSystemService<PatentDTO> _patentService;
        private readonly IFileSystemService<MagazineDTO> _magazineService;

        public FileStorageController(
            IFileSystemService<BookDTO> bookService, 
            IFileSystemService<LocalizedBookDTO> localizedBookService, 
            IFileSystemService<PatentDTO> patentService,
            IFileSystemService<MagazineDTO> magazineService)
        {
            _bookService = bookService;
            _localizedBookService = localizedBookService;
            _patentService = patentService;
            _magazineService = magazineService;
        }

        public void RunProgram()
        {
            InsertTestData();

            while (true)
            {
                Console.WriteLine("Enter type:");
                var type = Console.ReadLine();

                if (string.IsNullOrEmpty(type))
                {
                    Console.WriteLine("Provide file type.");
                    continue;
                }

                Console.WriteLine("Id :");

                var idChoice= Console.ReadKey(true).KeyChar;

                object result;

                if (char.IsWhiteSpace(idChoice))
                {
                    result = JsonConvert.SerializeObject(GetItems(type), Formatting.Indented);
                }
                else
                {
                    var id = (int)char.GetNumericValue(idChoice);

                    result = JsonConvert.SerializeObject(GetItem(type, id), Formatting.Indented);
                }

                Console.WriteLine(result);

                Console.WriteLine("Enter 1 if you want to exit:");
                var exit = Console.ReadKey(true).KeyChar;
                if (exit == '1')
                {
                    break;
                }
            }
        }

        public object GetItem(string type, int id)
        {
            object result = null;
            switch (type)
            {
                case "Book":
                {
                    result = _bookService.GetDocument(type, id);
                    break;
                }
                case "Localized Book":
                {
                    result = _localizedBookService.GetDocument(type, id);
                    break;
                }
                case "Patent":
                {
                    result = _patentService.GetDocument(type, id);
                    break;
                }
                case "Magazine":
                {
                    result = _magazineService.GetDocument(type, id);
                    break;
                }
            }

            return result;
        }

        private object GetItems(string type)
        {
            object result = null;
            switch (type)
            {
                case "Book":
                {
                    result = _bookService.GetAllDocuments(type);
                    break;
                }
                case "Localized Book":
                {
                    result = _localizedBookService.GetAllDocuments(type);
                    break;
                }
                case "Patent":
                {
                    result = _patentService.GetAllDocuments(type);
                    break;
                }
                case "Magazine":
                {
                    result = _magazineService.GetAllDocuments(type);
                    break;
                }
                default:
                {
                    break;
                }
            }

            return result;
        }

        private void InsertTestData()
        {
            foreach (var bookDto in GetFakeBookDtos())
            {
                _bookService.CreateDocument(bookDto);
            }

            foreach (var patentDto in GetFakePatentDtos())
            {
                _patentService.CreateDocument(patentDto);
            }
        }

        private static List<PublisherDTO> GetFakePublisherDtos()
        {
            return new List<PublisherDTO>()
            {
                new()
                {
                    Id = 0,
                    Name = "One"
                }
            };
        }

        private static List<AuthorDTO> GetFakeAuthorDtos()
        {
            return new List<AuthorDTO>()
            {
                new()
                {
                    Id = 0,
                    FirstName = "One",
                    LastName = "One"
                }
            };
        }

        private List<CountryDTO> GetFakeCountryDtos()
        {
            return new List<CountryDTO>()
            {
                new()
                {
                    Id = 0,
                    Name = "UA"
                }
            };
        }

        private List<PatentDTO> GetFakePatentDtos()
        {
            return new List<PatentDTO>()
            {
                new()
                {
                    Authors = GetFakeAuthorDtos(),
                    DatePublished = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddDays(10),
                    Id = 0,
                    Title = "One"
                },
                new()
                {
                    Authors = GetFakeAuthorDtos(),
                    DatePublished = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddMonths(1),
                    Id = 1
                }
            };
        }

        private static List<BookDTO> GetFakeBookDtos()
        {
            return new List<BookDTO>()
            {
                new()
                {
                    Authors = GetFakeAuthorDtos(),
                    DatePublished = DateTime.Now,
                    Id = 0,
                    ISBN = "123",
                    NumberOfPages = 123,
                    Publisher = GetFakePublisherDtos().First(),
                    Title = "12345"
                },
                new()
                {
                    Authors = GetFakeAuthorDtos(),
                    DatePublished = DateTime.Now.AddDays(-1),
                    Id = 1,
                    ISBN = "123",
                    NumberOfPages = 1000,
                    Publisher = GetFakePublisherDtos().First(),
                    Title = "oNE"
                }
            };
        }
    }
}
