namespace BLL.LibraryFileSystem.DTOs
{
    public class LocalizedBookDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public List<AuthorDTO> Authors { get; set; } = new();

        public PublisherDTO OriginalPublisher { get; set; } = new();

        public PublisherDTO LocalPublisher { get; set; } = new();

        public int NumberOfPages { get; set; }

        public CountryDTO CountryOfLocalization { get; set; } = new();

        public DateTime DatePublished { get; set; }
    }
}
