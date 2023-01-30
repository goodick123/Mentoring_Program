namespace BLL.LibraryFileSystem.DTOs
{
    public class MagazineDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public PublisherDTO Publisher { get; set; } = new();

        public int ReleaseNumber { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
