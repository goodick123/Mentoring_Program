namespace BLL.LibraryFileSystem.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public List<AuthorDTO> Authors { get; set; } = new();

        public PublisherDTO Publisher { get; set; } = new();

        public int NumberOfPages { get; set; }

        public DateTime DatePublished { get; set; }
    }
}
