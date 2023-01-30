using BLL.LibraryFileSystem.DTOs;
using BLL.LibraryFileSystem.Interfaces;
using Xunit;
using FluentAssertions;
using Moq;

namespace OOP.Test
{
    public class FileStorageControllerTests
    {
        public FileStorageController controller;

        private Mock<IFileSystemService<BookDTO>> _bookServiceMock;
        private Mock<IFileSystemService<LocalizedBookDTO>> _localizedBookServiceMock;
        private Mock<IFileSystemService<PatentDTO>> _patentServiceMock;
        private Mock<IFileSystemService<MagazineDTO>> _magazineServiceMock;

        public FileStorageControllerTests()
        {
            _bookServiceMock = new Mock<IFileSystemService<BookDTO>>();
            _localizedBookServiceMock = new Mock<IFileSystemService<LocalizedBookDTO>>();
            _patentServiceMock = new Mock<IFileSystemService<PatentDTO>>();
            _magazineServiceMock = new Mock<IFileSystemService<MagazineDTO>>();

            controller = new FileStorageController(_bookServiceMock.Object, _localizedBookServiceMock.Object, _patentServiceMock.Object, _magazineServiceMock.Object);
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

        [Theory]
        [InlineData("Book", 1)]
        public void Test1(string type, int id)
        {
            _bookServiceMock.Setup(x => x.GetDocument(type,id)).Returns(GetFakeBookDtos().Where(x=>x.Id == id).FirstOrDefault());

            var res = controller.GetItem(type,id);

            res.Should().BeEquivalentTo(GetFakeBookDtos().Where(x => x.Id == id).FirstOrDefault(),
                options => options.Excluding(a=>a.DatePublished));
        }
    }
}