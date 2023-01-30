using BLL.LibraryFileSystem.DTOs;

namespace OOP.Helpers
{
    public class FakeData
    {
        public static List<PublisherDTO> GetFakePublisherDtos()
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

        public static List<AuthorDTO> GetFakeAuthorDtos()
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

        public static List<CountryDTO> GetFakeCountryDtos()
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

        public static List<PatentDTO> GetFakePatentDtos()
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

        public static List<BookDTO> GetFakeBookDtos()
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
